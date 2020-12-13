using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameSelectionView : IOffsetView
{
    GameObject SelectGameObject(bool mode);
    void UnSelectGameObject(GameObject go, bool returned, bool inspector);
    void ActivateSelectable(int count);
    void ChangeMode(bool value);

    event EventHandler<GameObjectSelectedEventArgs> OnGameObjectSelected;
    event EventHandler<PapersReturnedEventArgs> OnPapersReturned;
}
