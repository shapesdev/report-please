using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CanBeReturnedEventArgs : EventArgs
{
    public bool canBeReturned;

    public CanBeReturnedEventArgs(bool value)
    {
        canBeReturned = value;
    }
}
