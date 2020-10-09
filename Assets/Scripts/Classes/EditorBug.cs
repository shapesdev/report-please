using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorBug : Bug
{
    public EditorBug(string title, int caseID, string testerName, string grabbag, string area, string reproSteps, string expectActual, string reproWith,
        bool regression, string FAV, bool publicField, int severity, int platform, int user, Tester tester)
    {
        this.title = title;
        this.caseID = caseID;
        this.testerName = testerName;
        this.grabbag = grabbag;
        this.area = area;

        this.reproSteps = reproSteps;
        expectedActualResults = expectActual;
        reproNoReproWith = reproWith;

        this.regression = regression;
        this.FAV = FAV;
        this.publicField = publicField;

        this.severity = severity;
        platformImportance = platform;
        userPrevalence = user;

        this.tester = tester;
    }

    public override ReportType GetReportType()
    {
        return ReportType.EditorBug;
    }
}