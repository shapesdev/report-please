using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportCard : Card
{
    [SerializeField]
    private ResponseDisplayer responseDisplay;
    [SerializeField]
    private EditorBugDisplayer editorDisplay;
    [SerializeField]
    private PackageButDisplayer packageDisplay;

    public override void ChangeSizeToLeft(IScenario scenario)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = paperLeft;

        if(scenario.GetReportType() == ReportType.Response)
        {
            responseDisplay.LeftDisplay();
        }
        else if(scenario.GetReportType() == ReportType.EditorBug)
        {
            editorDisplay.LeftDisplay();
        }
        else if(scenario.GetReportType() == ReportType.PackageBug)
        {
            packageDisplay.LeftDisplay();
        }
    }

    public override void ChangeSizeToRight(IScenario scenario)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = paperRight;

        if (scenario.GetReportType() == ReportType.Response)
        {
            responseDisplay.RightDisplay(scenario);
        }
        else if (scenario.GetReportType() == ReportType.EditorBug)
        {
            editorDisplay.RightDisplay(scenario);
        }
        else if (scenario.GetReportType() == ReportType.PackageBug)
        {
            packageDisplay.RightDisplay(scenario);
        }
    }
}
