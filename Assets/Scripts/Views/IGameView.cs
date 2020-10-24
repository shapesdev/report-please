using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameView : IOffsetView
{
    void Init(RuleBookSO ruleBook, DateTime date, IScenario scenario);
    void UpdateGameObjectPosition(Vector3 offset, bool offsetSet, GameObject go);

    event EventHandler<DragRightEventArgs> OnDragRight;
    event EventHandler<SpaceBarPressedEventArgs> OnSpaceBarPressed;
    event EventHandler<MousePressedEventArgs> OnMousePressed;
    event EventHandler<MouseReleasedEventArgs> OnMouseReleased;
    event EventHandler<MouseHoldEventArgs> OnMouseHold;
    event EventHandler<OffsetValueEventArgs> OnOffsetChanged;
}
