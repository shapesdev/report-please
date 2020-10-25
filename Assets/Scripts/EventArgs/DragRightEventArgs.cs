using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRightEventArgs : EventArgs
{
    public float panelWidth;
    public GameGeneralView card;

    public DragRightEventArgs(float width, GameGeneralView card)
    {
        panelWidth = width;
        this.card = card;
    }
}
