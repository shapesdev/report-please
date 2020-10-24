using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameObjectSelectedEventArgs : EventArgs
{
    public bool selected;

    public GameObjectSelectedEventArgs(bool selected)
    {
        this.selected = selected;
    }
}
