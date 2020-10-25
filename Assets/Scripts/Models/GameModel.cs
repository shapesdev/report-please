using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameModel: IGameModel
{
    public Dictionary<DateTime, List<IScenario>> DaysWithScenarios { get; }
    public GameGeneralView CurrentCard { get; set; }
    public DateTime CurrentDay { get; set; }
    public float CurrentPanelWidth { get; set; }
    public int CurrentScenario { get; set; } = 0;
    public bool Selected { get; set; }
    public bool OffsetSet { get; set; }
    public bool CanBeReturned { get; set; }
    public bool InspectorMode { get; set; }
    public Vector3 Offset { get; set; }
    public GameObject SelectedGameObject { get; set; }

    public GameModel()
    {
        DataInitialization dataInitialization = new DataInitialization();
        DaysWithScenarios = dataInitialization.GetDayData();
        CurrentDay = new DateTime(2020, 11, PlayerPrefs.GetInt("CurrentDay"));
    }
}
