using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OffsetSetEventArgs : EventArgs
{
    public bool offsetSet;

    public OffsetSetEventArgs(bool offsetSet)
    {
        this.offsetSet = offsetSet;
    }
}
