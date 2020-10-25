using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameView : IOffsetView
{
    void Init(DateTime date, IScenario scenario);
    void ShowScenario(IScenario scenario);
    void UpdateGameObjectPosition(Vector3 offset, bool offsetSet, GameObject go);
    void TurnOnInspectorMode();
    void TurnOffInspectorMode();

    event EventHandler<DragRightEventArgs> OnDragRight;
    event EventHandler<SpaceBarPressedEventArgs> OnSpaceBarPressed;
    event EventHandler<MousePressedEventArgs> OnMousePressed;
    event EventHandler<MouseReleasedEventArgs> OnMouseReleased;
    event EventHandler<MouseHoldEventArgs> OnMouseHold;
    event EventHandler<OffsetValueEventArgs> OnOffsetChanged;
}
