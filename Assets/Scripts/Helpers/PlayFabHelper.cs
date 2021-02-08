using UnityEngine;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System;
using PlayFab.Json;

public class PlayFabHelper : MonoBehaviour
{
    public static PlayFabHelper instance;

    private void Start()
    {
        instance = this;
        Login();
    }

    private void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier + 1,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    public void SaveScenarioData(List<LevelsStats> levelStats)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "ScenarioData", JsonConvert.SerializeObject(levelStats) }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    private void GetScenarioData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnScenarioDataReceived, OnError);
    }

    private void OnScenarioDataReceived(GetUserDataResult obj)
    {
        Debug.Log("Scenario Data Received");
        if(obj.Data != null && obj.Data.ContainsKey("ScenarioData"))
        {
            var data = JsonConvert.DeserializeObject<List<LevelsStats>>(obj.Data["ScenarioData"].Value);
            PlayerData.instance.SetPlayerData(data);
        }
    }

    public void AddCloudStatistics(int citationsReceived, int correctReportCount, int currentHighScore,
        int totalReportCount, int totalHighScore)
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "AddPlayerStats",
            FunctionParameter = new
            {
                citations = citationsReceived,
                correctReports = correctReportCount,
                highscore = currentHighScore,
                totalReports = totalReportCount,
                totalscore = totalHighScore
            },
            GeneratePlayStreamEvent = true,
        }, OnCloudUpdateStats, OnErrorShared);
    }

    public void UpdateCloudStatistics()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "UpdatePlayerStats",
            FunctionParameter = new
            {
                citations = PlayerData.instance.citationsReceived,
                correctReports = PlayerData.instance.correctReports,
                highscore = PlayerData.instance.currentHighScore,
            },
            GeneratePlayStreamEvent = true,
        }, OnCloudUpdateStats, OnErrorShared);
    }
    public void GetCloudStatistics()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStatistics,
            error => Debug.LogError(error.GenerateErrorReport())
            );
    }

    void OnGetStatistics(GetPlayerStatisticsResult result)
    {
        foreach (var eachStat in result.Statistics)
        {
            switch (eachStat.StatisticName)
            {
                case "CitationsReceived":
                    PlayerData.instance.citationsReceived = eachStat.Value;
                    break;
                case "CorrectReports":
                    PlayerData.instance.correctReports = eachStat.Value;
                    break;
                case "CurrentHighScore":
                    PlayerData.instance.currentHighScore = eachStat.Value;
                    break;
            }
        }
    }

    private void OnCloudUpdateStats(ExecuteCloudScriptResult result)
    {
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue);
    }

    private void OnErrorShared(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        Debug.Log("COULDNT UPDATE CLOUD");
    }

    private void OnSuccess(LoginResult obj)
    {
        Debug.Log("Successful Login");
        GetScenarioData();
        GetCloudStatistics();
    }

    private void OnError(PlayFabError obj)
    {
        Debug.Log(obj.ErrorDetails);
    }

    private void OnDataSend(UpdateUserDataResult obj)
    {
        Debug.Log("Update Successful");
    }
}
