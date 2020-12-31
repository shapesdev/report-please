using System;
using UnityEngine;

public interface ICharacterView
{
    void ShowTesterCharacter(IScenario scenario, Sprite sprite,
        IGameSelectionView selectionView, IDialogueView dialogueView, DateTime day);
}
