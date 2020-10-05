using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageBug : Bug
{
    private string package;
    private string packageVersion;

    public PackageBug(string title, int caseID, string tester, string grabbag, string area, string reproSteps, string expectActual, string reproWith,
    bool regression, string FAV, bool publicField, int severity, int platform, int user, string package, string packageVersion)
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

        this.package = package;
        this.packageVersion = packageVersion;
    }
}
