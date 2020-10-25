using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelectedEventArgs : EventArgs
{
    public GameObject go;

    public ObjectSelectedEventArgs(GameObject go)
    {
        this.go = go;
    }
}
