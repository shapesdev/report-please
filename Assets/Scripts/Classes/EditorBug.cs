using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorBug : Bug
{
    public EditorBug(string title, int caseID, string tester, string grabbag, string area, string reproSteps, string expectActual, string reproWith,
        bool regression, string FAV, bool publicField, int severity, int platform, int user)
    {
        this.title = title;
        this.caseID = caseID;
        this.tester = tester;
        this.grabbag = grabbag;
        this.area = area;

        this.reproSteps = reproSteps;
        this.expectedActualResults = expectActual;
        this.reproNoReproWith = reproWith;

        this.regression = regression;
        this.FAV = FAV;
        this.publicField = publicField;

        this.severity = severity;
        this.platformImportance = platform;
        this.userPrevalence = user;
    }
}