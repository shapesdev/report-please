using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bug : IScenario
{
    protected string title;
    protected int caseID;
    protected string testerName;

    protected string reproSteps;
    protected string expectedActualResults;
    protected string reproNoReproWith;

    protected bool regression;
    protected string FAV;
    protected bool publicField;

    [Range(0, 4)]
    protected int severity;
    [Range(0, 4)]
    protected int platformImportance;
    [Range(0, 4)]
    protected int userPrevalence;

    protected AreasNGrabbags area;
    protected Tester tester;

    public abstract ReportType GetReportType();

    public abstract Discrepancy GetDiscrepancy();

    public string GetTitle()
    {
        return title;
    }

    public int GetCaseID()
    {
        return caseID;
    }

    public string GetTesterName()
    {
        return testerName;
    }

    public AreasNGrabbags GetArea()
    {
        return area;
    }

    public string GetReproSteps()
    {
        return reproSteps;
    }

    public string GetExpectedActualResults()
    {
        return expectedActualResults;
    }

    public string GetReproNoReproWith()
    {
        return reproNoReproWith;
    }

    public bool IsRegression()
    {
        return regression;
    }

    public string GetFirstAffected()
    {
        return FAV;
    }

    public bool isPublic()
    {
        return publicField;
    }

    public int GetUserPrevalence()
    {
        return userPrevalence;
    }

    public int GetPlatformImportance()
    {
        return platformImportance;
    }

    public int GetSeverity()
    {
        return severity;
    }

    public Tester GetTester()
    {
        return tester;
    }
}