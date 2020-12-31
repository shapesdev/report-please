using System;
using UnityEngine;

public interface ICharacterView
{
    void ShowTesterCharacter(IScenario scenario, Sprite sprite, int current, int last,
        IGameSelectionView selectionView, DateTime day, Discrepancy discrepancy);
}
