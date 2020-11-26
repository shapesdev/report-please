using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ScenarioStats
{
    public string day;
    public int caseID;
    public string caseTitle;
    public string citation;
    public int score;

    public ScenarioStats(string day, int caseID, string caseTitle, string citation, int score)
    {
        this.day = day;
        this.caseID = caseID;
        this.caseTitle = caseTitle;
        this.citation = citation;
        this.score = score;
    }
}
