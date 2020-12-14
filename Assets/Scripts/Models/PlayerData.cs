using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    private List<LevelsStats> levelStats;

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
        PlayFabHelper.instance.SaveScenarioData(PlayerData.instance.GetPlayerData());
    }

    public List<LevelsStats> GetPlayerData()
    {
        return levelStats;
    }

    public void ClearPlayerData()
    {
        levelStats.Clear();
    }
}
