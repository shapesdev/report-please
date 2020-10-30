using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHelper : MonoBehaviour
{
    public static ColorHelper instance;

    public Color InspectorModeColor;
    public Color NormalModeColor;

    private void Start()
    {
        instance = this;
    }
}
