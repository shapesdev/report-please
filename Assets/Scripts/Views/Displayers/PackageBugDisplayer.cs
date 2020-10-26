using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection;
using System.Linq;
using System;

public class PackageBugDisplayer : GeneralDisplayer
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
    [SerializeField]
    private TMP_Text package;
    [SerializeField]
    private TMP_Text packageVersion;

    public override void Init(IScenario scenario)
    {
        if(scenario.GetReportType() == ReportType.PackageBug)
        {
            var bug = (PackageBug)scenario;

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
            package.text = "Package: " + bug.GetPackage();
            packageVersion.text = "Package Found Version: " + bug.GetPackageVersion();

            ConnectDataToFields(bug);
        }
    }

    private void ConnectDataToFields(PackageBug bug)
    {
        title.gameObject.GetComponent<FieldData>().SetData(bug.GetTitle());
        testerName.gameObject.GetComponent<FieldData>().SetData(bug.GetTesterName());
        reproSteps.gameObject.GetComponent<FieldData>().SetData(bug.GetReproSteps());
        expectedActual.gameObject.GetComponent<FieldData>().SetData(bug.GetExpectedActualResults());
        reproducible.gameObject.GetComponent<FieldData>().SetData(bug.GetReproNoReproWith());
        regression.gameObject.GetComponent<FieldData>().SetData(bug.IsRegression().ToString());
        publicField.gameObject.GetComponent<FieldData>().SetData(bug.isPublic().ToString());
        severity.gameObject.GetComponent<FieldData>().SetData(bug.GetSeverity().ToString());
        platform.gameObject.GetComponent<FieldData>().SetData(bug.GetPlatformImportance().ToString());
        userPrev.gameObject.GetComponent<FieldData>().SetData(bug.GetUserPrevalence().ToString());
        grabbag.gameObject.GetComponent<FieldData>().SetData(bug.GetArea().grabbag);
        area.gameObject.GetComponent<FieldData>().SetData(bug.GetArea().area);
        caseId.gameObject.GetComponent<FieldData>().SetData(bug.GetCaseID().ToString());
        FAV.gameObject.GetComponent<FieldData>().SetData(bug.GetFirstAffected());
        package.gameObject.GetComponent<FieldData>().SetData(bug.GetPackage());
        packageVersion.gameObject.GetComponent<FieldData>().SetData(bug.GetPackageVersion());
    }

    public override void RightDisplay(ReportType type)
    {
        if(type == ReportType.PackageBug) { gameObject.SetActive(true); }
    }

    public override void LeftDisplay(ReportType type)
    {
        if(type == ReportType.PackageBug) { gameObject.SetActive(false); }
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
        package.raycastTarget = true;
        packageVersion.raycastTarget = true;
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
        packageVersion.raycastTarget = false;
        package.raycastTarget = false;
    }
}
