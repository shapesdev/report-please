using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StampPressEventArgs : EventArgs
{
    public Sprite sprite;
    public Stamp stampType;

    public StampPressEventArgs(Sprite sprite, Stamp stampType)
    {
        this.sprite = sprite;
        this.stampType = stampType;
    }
}
