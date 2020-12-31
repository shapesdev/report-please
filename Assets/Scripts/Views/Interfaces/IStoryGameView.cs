using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStoryGameView : IOffsetView
{
    void Init(DateTime date, IScenario scenario);
    void ShowScenario(IScenario scenario, Sprite sprite, int current, int last,
        IGameSelectionView selectionView, DateTime day, Discrepancy discrepancy);
    void TurnOnInspectorMode();
    void TurnOffInspectorMode();
    void EnableCitation(string text);
    void ShowEndDay(int day, int curScore, int maxScore);
    void DisplayFieldText(string value);
    void TurnOffFieldText();

    event EventHandler<SpaceBarPressedEventArgs> OnSpaceBarPressed;
    event EventHandler<TabPressedEventArgs> OnTabPressed;
    event EventHandler<MousePressedEventArgs> OnMousePressed;
    event EventHandler<MouseReleasedEventArgs> OnMouseReleased;
    event EventHandler<MouseHoldEventArgs> OnMouseHold;
    event EventHandler<OffsetValueEventArgs> OnOffsetChanged;
    event EventHandler<StartScenarioShowingEventArgs> OnStartScenarioShowing;
    event EventHandler<ExportPressedEventArgs> OnExport;
}
