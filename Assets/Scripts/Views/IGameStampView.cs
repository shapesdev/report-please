using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameStampView 
{
    void PlaceStamp(GameObject selectedGameObject, Sprite sprite);
    void Reset();
    void ChangeMode(bool value);
    void ActivateStampPanel(bool value);

    event EventHandler<CanBeReturnedEventArgs> OnReturned;
    event EventHandler<StampPressEventArgs> OnStampPressed;
}
