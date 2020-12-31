using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameScenarioView : IGameGeneralView
{
    void Init(IScenario scenario);
}
