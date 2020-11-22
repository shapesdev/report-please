using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStoryGameModel
{
    Dictionary<DateTime, List<IScenario>> DaysWithScenarios { get; }
    List<Discrepancy> Discrepancies { get; }
    GameGeneralView CurrentGeneralView { get; set; }
    DateTime CurrentDay { get; set; }
    float CurrentPanelWidth { get; set; }
    int CurrentScenario { get; set; }
    bool Selected { get; set; }
    bool OffsetSet { get; set; }
    bool CanBeReturned { get; set; }
    bool InspectorMode { get; set; }
    Vector3 Offset { get; set; }
    GameObject SelectedGameObject { get; set; }
    RuleBookSO RuleBook { get; }
    bool DiscrepancyFound { get; set; }
    Stamp CurrentStamp { get; set; }
}
