using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelsStats
{
    public string day;
    public List<ScenarioStats> scenarios;

    public LevelsStats(string day, List<ScenarioStats> scenarios)
    {
        this.day = day;
        this.scenarios = scenarios;
    }
}
