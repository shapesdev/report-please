using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitationCheckController
{
    private Tuple<bool, string> isScenarioWithDiscrepancy;

    public Tuple<bool, string> CheckForCitations(IScenario curScenario, RuleBookSO ruleBook, bool issueFound)
    {
        if (curScenario.GetReportType() == ReportType.Response)
        {
            isScenarioWithDiscrepancy = ResponseChecker(curScenario, issueFound);
        }
        else if (curScenario.GetReportType() == ReportType.EditorBug)
        {
            isScenarioWithDiscrepancy = BugChecker(curScenario, issueFound, ruleBook, false);
        }
        else if (curScenario.GetReportType() == ReportType.PackageBug)
        {
            isScenarioWithDiscrepancy = BugChecker(curScenario, issueFound, ruleBook, true);
        }
        return isScenarioWithDiscrepancy;
    }

    private Tuple<bool, string> ResponseChecker(IScenario curScenario, bool issueFound)
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

            if (response.GetEmail().Contains("\n\n\n") || response.GetEmail().Contains("\n\n\n\n"))
            {
                string citation = "Report: Two empty lines between paragraphs";
                return Tuple.Create(true, citation);
            }

            if (spacesCount >= 2)
            {
                string citation = "Report: Double spaces between letters";
                return Tuple.Create(true, citation);
            }

            if (response.GetEmailSentFrom() != response.GetTester().GetEmail())
            {
                string citation = "Report: Wrong email address";
                return Tuple.Create(true, citation);
            }
            if (response.GetCloseType() == CloseType.NotQualified && (response.GetDateSent().Day - response.GetLastReplyDate().Day) >= 7)
            {
                string citation = "Report: Closed as Not Qualified too early";
                return Tuple.Create(true, citation);
            }
            if (response.WasItClosedCorrectly() == false)
            {
                string citation = "Report: Closed incorrectly";
                return Tuple.Create(true, citation);
            }
            if (!response.GetEmail().Contains(response.GetTester().GetName()))
            {
                string citation = "Report: Wrong tester name";
                return Tuple.Create(true, citation);
            }
            if (response.GetEmail().Contains("issuetracker") && !response.GetEmail().Contains(response.GetCaseID().ToString()))
            {
                string citation = "Report: Wrong issue tracker link";
                return Tuple.Create(true, citation);
            }
            if (response.GetTester().GetExpiryDate() < response.GetDateSent())
            {
                string citation = "Employee ID: Expired";
                return Tuple.Create(true, citation);
            }
        }

        string noCitation = "No citation";
        return Tuple.Create(false, noCitation);
    }

    private Tuple<bool, string> BugChecker(IScenario curScenario, bool issueFound, RuleBookSO ruleBook, bool isPackage)
    {
        Bug bug;

        if(isPackage)
        {
            bug = (PackageBug)curScenario;
        }
        else
        {
            bug = (EditorBug)curScenario;
        }

        var chars = bug.GetReproSteps().ToCharArray();
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

            if (spacesCount >= 2)
            {
                string citation = "Report: Double spaces between letters";
                return Tuple.Create(true, citation);
            }

            if (bug.isPublic() != true)
            {
                string citation = "Report: is Private and not Public";
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
            if(bug.IsRegression() == true)
            {
                bool containsAVersion = false;

                foreach (var stream in ruleBook.versionInfo)
                {
                    for(int i = 0; i < stream.versions.Length; i++)
                    {
                        if (bug.GetFirstAffected().Contains(stream.versions[i]))
                        {
                            containsAVersion = true;
                            break;
                        }
                    }
                }
                if(containsAVersion == false)
                {
                    string citation = "Report: Wrong FAV Field";
                    return Tuple.Create(true, citation);
                }
            }
            if(bug.IsRegression() == false)
            {
                bool containsAVersion = false;

                foreach (var stream in ruleBook.versionInfo)
                {
                    for (int i = 0; i < stream.versions.Length; i++)
                    {
                        if (bug.GetFirstAffected().Contains(stream.versions[i]))
                        {
                            containsAVersion = true;
                            break;
                        }
                    }
                }
                if (containsAVersion == true)
                {
                    string citation = "Report: Wrong FAV Field";
                    return Tuple.Create(true, citation);
                }
            }
            if(bug.GetSeverity() != bug.GetCorrectSeverity())
            {
                string citation = "Report: Wrong Severity";
                return Tuple.Create(true, citation);
            }
        }

        string noCitation = "No citation";
        return Tuple.Create(false, noCitation);
    }
}
