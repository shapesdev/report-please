using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameView
{
    void ChangeInspectorMode(bool mode);
    void Init(RuleBookSO ruleBook, DateTime date, IScenario scenario);
    event EventHandler<SpaceBarPressedEventArgs> OnSpaceBarPressed;
}
