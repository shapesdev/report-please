using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : IScenario
{
    public string title;
    public int caseID;
    public string tester;
    public string grabbag;
    public string area;

    public string reproSteps;
    public string expectedActualResults;
    public string reproNoReproWith;

    public bool regression;
    public string FAV;
    public bool publicField;

    [Range(0, 4)]
    public int severity;
    [Range(0, 4)]
    public int platformImportance;
    [Range(0, 4)]
    public int userPrevalence;
}