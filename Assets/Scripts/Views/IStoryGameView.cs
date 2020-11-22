using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStoryGameView : IOffsetView
{
    void Init(DateTime date, IScenario scenario);
    void ShowScenario(IScenario scenario);
    void UpdateGameObjectPosition(Vector3 offset, bool offsetSet, GameObject go);
    void TurnOnInspectorMode();
    void TurnOffInspectorMode();
    void EnableCitation(string text);
    void ShowEndDay(int day);
    void DisplayFieldText(string value);
    void TurnOffFieldText();

    event EventHandler<DragRightEventArgs> OnDragRight;
    event EventHandler<SpaceBarPressedEventArgs> OnSpaceBarPressed;
    event EventHandler<TabPressedEventArgs> OnTabPressed;
    event EventHandler<MousePressedEventArgs> OnMousePressed;
    event EventHandler<MouseReleasedEventArgs> OnMouseReleased;
    event EventHandler<MouseHoldEventArgs> OnMouseHold;
    event EventHandler<OffsetValueEventArgs> OnOffsetChanged;
}
