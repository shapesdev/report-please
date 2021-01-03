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
    protected int correctSeverity;
    protected int correctPlatform;

    protected AreasNGrabbags area;
    protected Tester tester;
    protected Discrepancy discrepancy;

    public abstract ReportType GetReportType();

    public abstract Discrepancy GetDiscrepancy();

    public int GetCorrectSeverity()
    {
        return correctSeverity;
    }

    public int GetCorrectPlatform()
    {
        return correctPlatform;
    }

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

    public bool IsEmployeeIdMissing()
    {
        if (discrepancy == null)
        {
            return false;
        }
        else
        {
            return (discrepancy.GetFirstTag() == "ValidID" && discrepancy.GetSecondTag() == "ValidIDRule") ||
                (discrepancy.GetFirstTag() == "ValidIDRule" && discrepancy.GetSecondTag() == "ValidID") ? true : false;
        }
    }

    public void SetTester(Tester tester)
    {
        this.tester = tester;
    }

    public void SetSeverity(int severity)
    {
        this.severity = severity;
    }

    public void SetFirstAffected(string FAV)
    {
        this.FAV = FAV;
    }

    public void SetPlatFormImportance(int importance)
    {
        platformImportance = importance;
    }

    public void SetRegression(bool value)
    {
        regression = value;
    }

    public void SetArea(AreasNGrabbags area)
    {
        this.area = area;
    }

    public void SetReportToPrivate()
    {
        publicField = false;
    }

    public void SetTesterName(string name)
    {
        testerName = name;
    }

    public void SetDiscrepancy(Discrepancy discrepancy)
    {
        this.discrepancy = discrepancy;
    }

    public void SetReproSteps(string reproSteps)
    {
        this.reproSteps = reproSteps;
    }

    public void SetExpectedActual(string expected)
    {
        expectedActualResults = expected;
    }
    public void SetReproStepsWith(string steps)
    {
        reproNoReproWith = steps;
    }
}