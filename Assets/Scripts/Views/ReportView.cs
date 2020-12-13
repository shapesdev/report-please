using UnityEngine;
using UnityEngine.UI;

public class ReportView : GameGeneralView, IGameScenarioView
{
    [SerializeField]
    private GeneralDisplayer[] generalDisplayers;

    ReportType currentType;

    public void Init(IScenario scenario)
    {
        currentType = scenario.GetReportType();

        foreach(var displayers in generalDisplayers)
        {
            displayers.Init(scenario);
        }
    }

    public override void ChangeSizeToLeft()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = paperLeft;

        foreach (var displayer in generalDisplayers)
        {
            displayer.LeftDisplay(currentType);
        }
    }

    public override void ChangeSizeToRight()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = paperRight;

        foreach (var displayer in generalDisplayers)
        {
            displayer.RightDisplay(currentType);
        }
    }

    public override void TurnOffInspectorMode()
    {
        gameObject.GetComponent<Image>().raycastTarget = true;
        gameObject.GetComponent<Image>().color = ColorHelper.instance.NormalModeColor;

        foreach (var displayer in generalDisplayers)
        {
            displayer.TurnOffRaycast();
        }
    }

    public override void TurnOnInspectorMode()
    {
        gameObject.GetComponent<Image>().raycastTarget = false;
        gameObject.GetComponent<Image>().color = ColorHelper.instance.InspectorModeColor;

        foreach (var displayer in generalDisplayers)
        {
            displayer.TurnOnRaycast();
        }
    }
}
