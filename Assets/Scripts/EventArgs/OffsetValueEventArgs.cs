using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OffsetValueEventArgs : EventArgs
{
    public Vector3 offset;

    public OffsetValueEventArgs(Vector3 offset)
    {
        this.offset = offset;
    }
}
