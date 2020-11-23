using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHelper
{
    private LevelStats levelStats;

    public StatsHelper()
    {
        if(PlayerPrefs.GetString("Stats") == "")
        {
            LoadDefaultStats();
        }
        else
        {
            DeserializeFromJson();
        }
    }

    public void AddLevel(ScenarioStats scenarioStats)
    {
        levelStats.levels.Add(scenarioStats);
    }

    public List<ScenarioStats> GetLevels()
    {
        return levelStats.levels;
    }

    public void SaveLevels()
    {
        SerializeToJson();
    }

    private void LoadDefaultStats()
    {
        levelStats = new LevelStats();
        SerializeToJson();
    }

    private void SerializeToJson()
    {
        string jsonString = JsonUtility.ToJson(levelStats);
        PlayerPrefs.SetString("Stats", jsonString);
    }

    private void DeserializeFromJson()
    {
        string jsonString = PlayerPrefs.GetString("Stats");
        levelStats = (LevelStats)JsonUtility.FromJson(jsonString, typeof(LevelStats));
    }
}
