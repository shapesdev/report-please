using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitationCheckController
{
    private Tuple<bool, string> isScenarioWithDiscrepancy;

    public Tuple<bool, string> CheckForCitations(IScenario curScenario, RuleBookSO ruleBook, bool issueFound, Stamp currentStamp)
    {
        if (curScenario.GetReportType() == ReportType.Response)
        {
            isScenarioWithDiscrepancy = ResponseChecker(curScenario, issueFound, currentStamp);
        }
        else if (curScenario.GetReportType() == ReportType.EditorBug)
        {
            isScenarioWithDiscrepancy = BugChecker(curScenario, issueFound, ruleBook, false, currentStamp);
        }
        else if (curScenario.GetReportType() == ReportType.PackageBug)
        {
            isScenarioWithDiscrepancy = BugChecker(curScenario, issueFound, ruleBook, true, currentStamp);
        }
        return isScenarioWithDiscrepancy;
    }

    private Tuple<bool, string> ResponseChecker(IScenario curScenario, bool issueFound, Stamp stamp)
    {
        var response = (Response)curScenario;
        var chars = response.GetEmail().ToCharArray();
        int spacesCount = 0;

        if (issueFound == false)
        {
            foreach (var ch in chars)
            {
                if (ch == ' ')
                {
                    spacesCount++;
                }
                else
                {
                    spacesCount = 0;
                }

                if (spacesCount >= 2)
                {
                    break;
                }
            }
            if (stamp == Stamp.SIREN)
            {
                if (curScenario.GetDiscrepancy() == null)
                {
                    string citation = "Correct Report Denied";
                    return Tuple.Create(true, citation);
                }
                else if(curScenario.GetDiscrepancy() != null)
                {
                    string citation = "No Citation";
                    return Tuple.Create(false, citation);
                }
            }
            if (response.GetEmail().Contains("\n\n\n") || response.GetEmail().Contains("\n\n\n\n"))
            {
                string citation = "Report: Two Empty Lines Between Paragraphs";
                return Tuple.Create(true, citation);
            }

            if (spacesCount >= 2)
            {
                string citation = "Report  : Double  Spaces  Between  Letters";
                return Tuple.Create(true, citation);
            }

            if (response.GetEmailSentFrom() != response.GetTester().GetEmail())
            {
                string citation = "Report: Wrong Email Address";
                return Tuple.Create(true, citation);
            }
            if (response.GetCloseType() == CloseType.NotQualified && (response.GetDateSent().Day - response.GetLastReplyDate().Day) >= 7)
            {
                string citation = "Report: Closed as Not Qualified too early";
                return Tuple.Create(true, citation);
            }
            if (response.WasItClosedCorrectly() == false)
            {
                string citation = "Report: Closed Incorrectly";
                return Tuple.Create(true, citation);
            }
            if (!response.GetEmail().Contains(response.GetTester().GetName()))
            {
                string citation = "Report: Wrong Tester Name";
                return Tuple.Create(true, citation);
            }
            if (response.GetEmail().Contains("issuetracker") && !response.GetEmail().Contains(response.GetCaseID().ToString()))
            {
                string citation = "Report: Wrong Issue Tracker Link";
                return Tuple.Create(true, citation);
            }
            if (response.GetTester().GetExpiryDate() < response.GetDateSent())
            {
                string citation = "Employee ID: Expired";
                return Tuple.Create(true, citation);
            }
        }
        else if (issueFound)
        {
            if (stamp == Stamp.PLUS && curScenario.GetDiscrepancy() != null)
            {
                string citation = "Wrong Report Approved";
                return Tuple.Create(true, citation);
            }
        }

        string noCitation = "No Citation";
        return Tuple.Create(false, noCitation);
    }

    private Tuple<bool, string> BugChecker(IScenario curScenario, bool issueFound, RuleBookSO ruleBook, bool isPackage, Stamp stamp)
    {
        Bug bug;

        if(isPackage) { bug = (PackageBug)curScenario; }
        else { bug = (EditorBug)curScenario; }

        bool correctRegression = false;
        bool wrongFav = false;

        if (issueFound == false)
        {
            if (stamp == Stamp.SIREN)
            {
                if(curScenario.GetDiscrepancy() == null)
                {
                    string citation = "Correct Report Denied";
                    return Tuple.Create(true, citation);
                }
            }
            if (bug.isPublic() != true)
            {
                string citation = "Report: Is Marked As Private";
                return Tuple.Create(true, citation);
            }
            if(bug.GetTesterName() != bug.GetTester().GetName())
            {
                string citation = "Report: Wrong Tester Name";
                return Tuple.Create(true, citation);
            }
            if(bug.GetTesterName() == string.Empty)
            {
                string citation = "Report: Empty Tester Name";
                return Tuple.Create(true, citation);
            }
            if(bug.GetReproSteps().Contains("\n\n\n") || bug.GetReproNoReproWith().Contains("\n\n\n") || bug.GetExpectedActualResults().Contains("\n\n\n"))
            {
                string citation = "Report: Empty line between paragraphs";
                return Tuple.Create(true, citation);
            }
            for(int i = 0; i < ruleBook.areas.Count; i++)
            {
                if(ruleBook.areas[i].area == bug.GetArea().area && ruleBook.areas[i].grabbag == bug.GetArea().grabbag)
                {
                    break;
                }
                if(i == ruleBook.areas.Count - 1)
                {
                    string citation = "Report: Wrong Area/Grabbag";
                    return Tuple.Create(true, citation);
                }
            }
            if(bug.GetPlatformImportance() != bug.GetCorrectPlatform())
            {
                string citation = "Report: Wrong Platform Importance";
                return Tuple.Create(true, citation);
            }
            if(!bug.GetReproSteps().Contains("How to reproduce"))
            {
                string citation = "Report: Wrong Report Order";
                return Tuple.Create(true, citation);
            }
            foreach (var stream in ruleBook.versionInfo)
            {
                for (int i = 0; i < stream.versions.Length; i++)
                {
                    if (bug.GetFirstAffected().Contains(stream.versions[i]))
                    {
                        wrongFav = true;

                        var strings = bug.GetReproNoReproWith().Split(new[] { "\n" }, StringSplitOptions.None);

                        if (i > 1)
                        {
                            if (strings[0].Contains(stream.versions[i]) && strings[1].Contains(stream.versions[i - 1]))
                            {
                                correctRegression = true;
                            }
                        }
                    }
                }
            }
            if(bug.IsRegression())
            {
                if(!correctRegression)
                {
                    if(isPackage)
                    {
                        var strings = bug.GetReproNoReproWith().Split(new[] { "\n" }, StringSplitOptions.None);

                        var bugas = (PackageBug)bug;
                        bool correctPackageRegression = false;

                        string regressionVersion = string.Empty;

                        foreach(var package in ruleBook.packagesInfo)
                        {
                            if(correctPackageRegression == false)
                            {
                                for (int i = package.versions.Length - 1; i >= 0; i--)
                                {
                                    if (strings[0].Contains(package.versions[i]))
                                    {
                                        regressionVersion = package.versions[i];
                                    }
                                    else
                                    {
                                        if (regressionVersion == bugas.GetPackageVersion())
                                        {
                                            correctPackageRegression = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        if(!correctPackageRegression)
                        {
                            string citation = "Report: Wrong Package Found Version";
                            return Tuple.Create(true, citation);
                        }
                    }
                    else
                    {
                        string citation = "Report: Wrong Regression field";
                        return Tuple.Create(true, citation);
                    }
                }
            }
            if(!bug.IsRegression())
            {
                if(wrongFav == true)
                {
                    string citation = "Report: Wrong FAV Field";
                    return Tuple.Create(true, citation);
                }
            }
            foreach(var stream in ruleBook.versionInfo)
            {
                if(!bug.GetReproNoReproWith().Contains(stream.stream))
                {
                    string citation = "Report: Not All Versions Tested";
                    return Tuple.Create(true, citation);
                }
            }
            if (bug.GetSeverity() != bug.GetCorrectSeverity())
            {
                string citation = "Report: Wrong Severity";
                return Tuple.Create(true, citation);
            }
            if (stamp == Stamp.PLUS && curScenario.GetDiscrepancy() != null)
            {
                string citation = "Wrong Report Approved";
                return Tuple.Create(true, citation);
            }
        }
        else if (issueFound)
        {
            if (stamp == Stamp.PLUS && curScenario.GetDiscrepancy() != null)
            {
                string citation = "Wrong Report Approved";
                return Tuple.Create(true, citation);
            }
        }

        string noCitation = "No Citation";
        return Tuple.Create(false, noCitation);
    }
}
