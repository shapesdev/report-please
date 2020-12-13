using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelStats
{
    public string day;
    public List<ScenarioStats> scenarios;

    public LevelStats(string day, List<ScenarioStats> scenarios)
    {
        this.day = day;
        this.scenarios = scenarios;
    }
}
