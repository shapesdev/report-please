using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public abstract class GameGeneralView : MonoBehaviour, IGameGeneralView
{
    public Vector2 paperRight;
    public Vector2 paperLeft;

    public float sizeChangeOffsetRight = 80f;
    public float sizeChangeOffsetLeft = 20f;

    public abstract void ChangeSizeToLeft();
    public abstract void ChangeSizeToRight();
    public abstract void TurnOffInspectorMode();
    public abstract void TurnOnInspectorMode();

    public void Check(float panelWidth)
    {
        if (transform.localPosition.x >= -Screen.width / 2 + panelWidth + sizeChangeOffsetRight)
        {
            ChangeSizeToRight();
        }
        else if (transform.localPosition.x <= Screen.width / 2 - panelWidth - sizeChangeOffsetLeft)
        {
            ChangeSizeToLeft();
        }
    }
}
