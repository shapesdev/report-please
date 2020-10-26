using UnityEngine;
using TMPro;

public class EditorBugDisplayer : GeneralDisplayer
{
    [SerializeField]
    private TMP_Text title;
    [SerializeField]
    private TMP_Text testerName;
    [SerializeField]
    private TMP_Text reproSteps;
    [SerializeField]
    private TMP_Text expectedActual;
    [SerializeField]
    private TMP_Text reproducible;
    [SerializeField]
    private TMP_Text regression;
    [SerializeField]
    private TMP_Text publicField;
    [SerializeField]
    private TMP_Text severity;
    [SerializeField]
    private TMP_Text platform;
    [SerializeField]
    private TMP_Text userPrev;
    [SerializeField]
    private TMP_Text grabbag;
    [SerializeField]
    private TMP_Text area;
    [SerializeField]
    private TMP_Text caseId;
    [SerializeField]
    private TMP_Text FAV;

    public override void Init(IScenario scenario)
    {
        if (scenario.GetReportType() == ReportType.EditorBug)
        {
            var bug = (EditorBug)scenario;

            title.text = bug.GetTitle();
            testerName.text = bug.GetTesterName();
            reproSteps.text = bug.GetReproSteps();
            expectedActual.text = bug.GetExpectedActualResults();
            reproducible.text = bug.GetReproNoReproWith();
            if (bug.IsRegression()) { regression.text = "Regression: Yes"; } else { regression.text = "Regression: No"; }
            if (bug.isPublic()) { publicField.text = "Public: Yes"; } else { publicField.text = "Public: No"; }
            severity.text = "Severity: " + bug.GetSeverity();
            platform.text = "Platform Importance: " + bug.GetPlatformImportance();
            userPrev.text = "User Prevalence: " + bug.GetUserPrevalence();
            grabbag.text = bug.GetArea().grabbag;
            area.text = bug.GetArea().area;
            caseId.text = bug.GetCaseID().ToString();
            FAV.text = bug.GetFirstAffected();
        }
    }

    public override void LeftDisplay(ReportType type)
    {
        if(type == ReportType.EditorBug) { gameObject.SetActive(false); }
    }

    public override void RightDisplay(ReportType type)
    {
        if(type == ReportType.EditorBug) { gameObject.SetActive(true); }
    }

    public override void TurnOnRaycast()
    {
        title.raycastTarget = true;
        testerName.raycastTarget = true;
        reproSteps.raycastTarget = true;
        expectedActual.raycastTarget = true;
        reproducible.raycastTarget = true;
        regression.raycastTarget = true;
        publicField.raycastTarget = true;
        severity.raycastTarget = true;
        platform.raycastTarget = true;
        userPrev.raycastTarget = true;
        grabbag.raycastTarget = true;
        area.raycastTarget = true;
        caseId.raycastTarget = true;
        FAV.raycastTarget = true;
    }

    public override void TurnOffRaycast()
    {
        title.raycastTarget = false;
        testerName.raycastTarget = false;
        reproSteps.raycastTarget = false;
        expectedActual.raycastTarget = false;
        reproducible.raycastTarget = false;
        regression.raycastTarget = false;
        publicField.raycastTarget = false;
        severity.raycastTarget = false;
        platform.raycastTarget = false;
        userPrev.raycastTarget = false;
        grabbag.raycastTarget = false;
        area.raycastTarget = false;
        caseId.raycastTarget = false;
        FAV.raycastTarget = false;
    }
}
