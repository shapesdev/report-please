using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDisplayer
{
    void TurnOnInspectorMode();
    void TurnOffInspectorMode();
    void SetData(IScenario scenario);
}
