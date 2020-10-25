using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameSelectionView : IOffsetView
{
    GameObject SelectGameObject(bool mode);
    void UnSelectGameObject(GameObject go, bool returned);
    void ActivateSelectable();
    void ChangeMode(bool value);

    event EventHandler<GameObjectSelectedEventArgs> OnGameObjectSelected;
    event EventHandler<PapersReturnedEventArgs> OnPapersReturned;
}
