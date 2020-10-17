using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public override void TurnOffInspectorMode()
    {
        gameObject.GetComponent<Image>().raycastTarget = true;

        responseDisplay.TurnOffInspectorMode();
        editorDisplay.TurnOffInspectorMode();
        packageDisplay.TurnOffInspectorMode();
    }

    public override void TurnOnInspectorMode()
    {
        gameObject.GetComponent<Image>().raycastTarget = false;

        responseDisplay.TurnOnInspectorMode();
        editorDisplay.TurnOnInspectorMode();
        packageDisplay.TurnOnInspectorMode();
    }
}
