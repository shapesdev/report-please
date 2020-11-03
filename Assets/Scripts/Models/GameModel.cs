using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameModel: IGameModel
{
    private DateTime currentDay;

    public Dictionary<DateTime, List<IScenario>> DaysWithScenarios { get; }
    public List<Discrepancy> Discrepancies { get; }
    public GameGeneralView CurrentCard { get; set; }
    public DateTime CurrentDay { get
        {
            return currentDay;
        }
        set
        {
            currentDay = value;
            PlayerPrefs.SetInt("CurrentDay", currentDay.Day);
        }
    }
    public float CurrentPanelWidth { get; set; }
    public int CurrentScenario { get; set; } = 0;
    public bool Selected { get; set; }
    public bool OffsetSet { get; set; }
    public bool CanBeReturned { get; set; }
    public bool InspectorMode { get; set; }
    public Vector3 Offset { get; set; }
    public GameObject SelectedGameObject { get; set; }

    public RuleBookSO RuleBook { get; }
    public bool DiscrepancyFound { get; set; }

    public Stamp CurrentStamp { get; set; }

    public GameModel(RuleBookSO ruleBook)
    {
        RuleBook = ruleBook;
        DataInitialization dataInitialization = new DataInitialization();
        DaysWithScenarios = dataInitialization.GetDayData();
        Discrepancies = dataInitialization.GetAllDiscrepancies();
        CurrentDay = new DateTime(2020, 11, PlayerPrefs.GetInt("CurrentDay"));
    }
}
