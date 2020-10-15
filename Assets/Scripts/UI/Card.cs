using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public abstract class Card : MonoBehaviour
{
    public Vector2 paperRight;
    public Vector2 paperLeft;

    public float sizeChangeOffsetRight = 80f;
    public float sizeChangeOffsetLeft = 20f;

    public abstract void ChangeSizeToRight(IScenario scenario);
    public abstract void ChangeSizeToLeft(IScenario scenario);

    public void Check(float panelWidth, IScenario scenario)
    {
        if (transform.localPosition.x >= -Screen.width / 2 + panelWidth + sizeChangeOffsetRight)
        {
            ChangeSizeToRight(scenario);
        }
        else if (transform.localPosition.x <= Screen.width / 2 - panelWidth - sizeChangeOffsetLeft)
        {
            ChangeSizeToLeft(scenario);
        }
    }
}
