using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameSelectionView : IOffsetView
{
    GameObject SelectGameObject(bool mode);
    void UnSelectGameObject();

    event EventHandler<GameObjectSelectedEventArgs> OnGameObjectSelected;
}
