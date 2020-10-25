using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StampPressEventArgs : EventArgs
{
    public Sprite sprite;

    public StampPressEventArgs(Sprite sprite)
    {
        this.sprite = sprite;
    }
}
