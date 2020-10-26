using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DataCheckController
{
    private Tuple<bool, string> isScenarioWithDiscrepancy;
    private bool issueFound = false;

    public void Reset()
    {
        issueFound = false;
    }

    public Tuple<bool, bool> CheckFields(GameObject field1, GameObject field2, IScenario curScenario)
    {
        var value1 = field1.GetComponent<FieldData>();
        var value2 = field2.GetComponent<FieldData>();

        bool correlation = false;
        bool discrepancy = false;
    
        if(field1.tag == field2.tag)
        {
            correlation = true;

            if(field1.tag == "Email")
            {
                var response = (Response)curScenario;

                
            }

            if(value1.GetData() != value2.GetData())
            {
                discrepancy = true;
            }
        }

        return Tuple.Create(correlation, discrepancy);
    }

    private bool CheckEmail(Response response)
    {
        if(response.GetEmail() == response.GetTester().GetEmail())
        {
            return true;
        }
        return false;
    }

    public Tuple<bool, string> CheckForCitations(IScenario curScenario)
    {
        if(curScenario.GetReportType() == ReportType.Response)
        {
            isScenarioWithDiscrepancy = ResponseChecker(curScenario);
        }
        else if(curScenario.GetReportType() == ReportType.EditorBug)
        {
            //isScenarioWithDiscrepancy = BugChecker()
        }
        else if(curScenario.GetReportType() == ReportType.PackageBug)
        {
            //isScenarioWithDiscrepancy = PackageBugChecker();
        }

        issueFound = false;
        return isScenarioWithDiscrepancy;
    }

    private Tuple<bool, string> ResponseChecker(IScenario curScenario)
    {
        var response = (Response)curScenario;
        var chars = response.GetEmail().ToCharArray();
        int spacesCount = 0;

        if(issueFound == false)
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

            if (CheckEmail(response) == false)
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



/*    private void BugChecker(int day)
    {
        for (int i = 0; i < days[day].Count; i++)
        {
            var value = days[day][i];

            if (value.GetReportType() == ReportType.EditorBug)
            {
                var bug = (EditorBug)value;

                bool hasCorrectBoth = false;
                bool hasVersionsWhenNoRegression = false;
                bool hasNoVersionButIsRegression = false;

                foreach (var area in areas)
                {
                    if (area.area == bug.GetArea().area && area.grabbag == bug.GetArea().grabbag)
                    {
                        hasCorrectBoth = true;
                        break;
                    }
                }

                var stringWithoutFAV = bug.GetFirstAffected().Substring(5, bug.GetFirstAffected().Length - 5);
                var strings = stringWithoutFAV.Split(',');

                foreach (var str in strings)
                {
                    if (str.Length > 8 && bug.IsRegression() == false)
                    {
                        hasVersionsWhenNoRegression = true;
                    }
                    else if (str.Length < 8 && bug.IsRegression() == true)
                    {
                        hasNoVersionButIsRegression = true;
                    }
                }

                if (hasVersionsWhenNoRegression)
                {
                    Debug.Log("Day " + day + " Scenario number " + i + "FAV has a specified version, but no regression");
                    continue;
                }
                if (hasNoVersionButIsRegression)
                {
                    Debug.Log("Day " + day + " Scenario number " + i + "Regression if specified but no regression found");
                    continue;
                }
                if (hasCorrectBoth == false)
                {
                    Debug.Log("Day " + day + " Scenario number " + i + "has incorrect Area/Grabbag");
                    continue;
                }

                if (bug.GetReproSteps().Contains("Actual") || bug.GetReproSteps().Contains("Reproducible"))
                {
                    Debug.Log("Day " + day + " Scenario number " + i + "has incorrect report order");
                    continue;
                }
                if (bug.isPublic() == false)
                {
                    Debug.Log("Day " + day + " Scenario number " + i + "should be public not private");
                    continue;
                }
                if ((bug.GetTitle().Contains("WebGL") && bug.GetPlatformImportance() != 1) || (bug.GetTitle().Contains("Player")) &&
                    bug.GetPlatformImportance() != 2)
                {
                    Debug.Log("Day " + day + " Scenario number " + i + "Bad Platform Importance selected");
                    continue;
                }
                if (bug.GetSeverity() == 1 && !bug.GetExpectedActualResults().Contains("crash"))
                {
                    Debug.Log("Day " + day + " Scenario number " + i + "Wrong severity is selected");
                    continue;
                }
                Debug.Log("Day " + day + " Scenario number " + i + "is a good report");
            }

            if (value.GetReportType() == ReportType.PackageBug)
            {
                var bug = (PackageBug)value;

                bool hasCorrectBoth = false;

                foreach (var area in areas)
                {
                    if (area.area == bug.GetArea().area && area.grabbag == bug.GetArea().grabbag)
                    {
                        hasCorrectBoth = true;
                        break;
                    }
                }

                if (hasCorrectBoth == false)
                {
                    Debug.Log("Day " + day + " Scenario number " + i + "has incorrect Area/Grabbag");
                    continue;
                }
                if (bug.GetReproSteps().Contains("Actual") || bug.GetReproSteps().Contains("Reproducible"))
                {
                    Debug.Log("Day " + day + " Scenario number " + i + "has incorrect report order");
                    continue;
                }
                if (bug.isPublic() == false)
                {
                    Debug.Log("Day " + day + " Scenario number " + i + "should be public not private");
                    continue;
                }
                if ((bug.GetTitle().Contains("WebGL") && bug.GetPlatformImportance() != 1) || (bug.GetTitle().Contains("Player")) &&
                    bug.GetPlatformImportance() != 2)
                {
                    Debug.Log("Day " + day + " Scenario number " + i + "Bad Platform Importance selected");
                    continue;
                }
                if (bug.GetSeverity() == 1 && !bug.GetExpectedActualResults().Contains("crash"))
                {
                    Debug.Log("Day " + day + " Scenario number " + i + "Wrong severity is selected");
                    continue;
                }
                Debug.Log("Day " + day + " Scenario number " + i + "is a good report");
            }
        }
    }*/
}
