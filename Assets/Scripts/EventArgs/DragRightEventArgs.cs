using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRightEventArgs : EventArgs
{
    public float panelWidth;
    public Card card;

    public DragRightEventArgs(float width, Card card)
    {
        panelWidth = width;
        this.card = card;
    }
}
