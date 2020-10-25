using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneralDisplayer : MonoBehaviour
{
    public abstract void Init(IScenario scenario);
    public abstract void RightDisplay(ReportType type);
    public abstract void LeftDisplay(ReportType type);
    public abstract void TurnOnRaycast();
    public abstract void TurnOffRaycast();
}
