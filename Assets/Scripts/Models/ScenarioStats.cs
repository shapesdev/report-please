using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ScenarioStats
{
    public DateTime day;
    public int caseID { get; set; }
    public string caseTitle { get; set; }
    public string citation { get; set; }
    public int score { get; set; }

    public ScenarioStats(DateTime day, int caseID, string caseTitle, string citation, int score)
    {
        this.day = day;
        this.caseID = caseID;
        this.caseTitle = caseTitle;
        this.score = score;
    }
}
