using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameModel: IGameModel
{
    private RuleBookSO ruleBook;

    private bool inspectorMode;

    public event EventHandler<InspectorModeEventArgs> OnInspectorModeActivated = (sender, e) => { };

    public Dictionary<DateTime, List<IScenario>> DaysWithScenarios { get; }
    public Card CurrentCard { get; set; }
    public DateTime CurrentDay { get; set; }
    public float CurrentPanelWidth { get; set; }
    public int CurrentScenario { get; set; } = 0;
    public bool Selected { get; set; }
    public bool OffsetSet { get; set; }
    public bool CanBeReturned { get; set; }
    public bool InspectorMode { get { return inspectorMode; }
        set {

            if(inspectorMode != value)
            {
                inspectorMode = value;

                var eventArgs = new InspectorModeEventArgs();
                OnInspectorModeActivated(this, eventArgs);
            }
        }
    }
    public Vector3 Offset { get; set; }

    public RuleBookSO RuleBook => ruleBook;

    public GameModel(RuleBookSO ruleBook)
    {
        this.ruleBook = ruleBook;
        DataInitialization dataInitialization = new DataInitialization();
        DaysWithScenarios = dataInitialization.GetDayData();
        CurrentDay = new DateTime(2020, 11, PlayerPrefs.GetInt("CurrentDay"));
    }
}
