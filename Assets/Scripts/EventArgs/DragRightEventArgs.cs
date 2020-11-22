using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRightEventArgs : EventArgs
{
    public float panelWidth;
    public GameGeneralView generalView;

    public DragRightEventArgs(float width, GameGeneralView generalView)
    {
        panelWidth = width;
        this.generalView = generalView;
    }
}
