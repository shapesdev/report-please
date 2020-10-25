using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameGeneralView
{
    void ChangeSizeToRight();
     void ChangeSizeToLeft();
    void TurnOnInspectorMode();
    void TurnOffInspectorMode();
    void Check(float width);
}
