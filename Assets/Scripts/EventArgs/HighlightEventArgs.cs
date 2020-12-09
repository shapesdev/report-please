using System;
using UnityEngine;

public class HighlightEventArgs : EventArgs
{
    public bool isHighlight;
    public GameObject goToHighlight;

    public HighlightEventArgs(bool value, GameObject go)
    {
        isHighlight = value;
        goToHighlight = go;
    }
}
