using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseHoldEventArgs : EventArgs
{
    public float width;

    public MouseHoldEventArgs(float width)
    {
        this.width = width;
    }
}
