using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    private List<LevelsStats> levelStats;

    public int citationsReceived = 0;
    public int correctReports = 0;
    public int currentHighScore = 0;
    public int maxHighScore = 0;
    public int maxReports = 0;

    private void Awake()
    {
        instance = this;
        levelStats = new List<LevelsStats>();
    }

    public void SetPlayerData(List<LevelsStats> levelStats)
    {
        this.levelStats = levelStats;
    }

    public void AddPlayerData(LevelsStats level)
    {
        levelStats.Add(level);
        PlayFabHelper.instance.SaveScenarioData(levelStats);
        PlayFabHelper.instance.UpdateCloudStatistics();
    }

    public List<LevelsStats> GetPlayerData()
    {
        return levelStats;
    }

    public void ClearPlayerData()
    {
        levelStats.Clear();
        citationsReceived = 0;
        correctReports = 0;
        currentHighScore = 0;
        PlayFabHelper.instance.SaveScenarioData(levelStats);
    }
}
