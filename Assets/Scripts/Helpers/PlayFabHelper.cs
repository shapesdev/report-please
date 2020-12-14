using UnityEngine;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System;

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
            CustomId = SystemInfo.deviceUniqueIdentifier,
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

    private void OnSuccess(LoginResult obj)
    {
        Debug.Log("Successful Login");
        GetScenarioData();
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
