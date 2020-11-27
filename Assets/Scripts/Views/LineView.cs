using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LineView : MonoBehaviour, ILineView
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private LineRenderer[] borderLineRenderers;

    public static event Action<int> OnHighlight;

    public void CheckFieldHighlight(bool isHighlight, GameObject selection, bool inspector)
    {
        if(inspector)
        {
            if (isHighlight)
            {
                OnHighlight?.Invoke(0);
                Highlight(selection, ColorHelper.instance.NormalModeColor);
            }
            else
            {
                OnHighlight?.Invoke(1);
                Highlight(selection, ColorHelper.instance.InspectorModeColor);
            }
        }
        else
        {
            Highlight(selection, ColorHelper.instance.NormalModeColor);
        }
    }

    private void Highlight(GameObject selection, Color color)
    {
        var text = selection.GetComponent<TMP_Text>();

        if (text == null)
        {
            var img = selection.GetComponent<Image>();
            img.color = color;
        }
        else
        {
            text.color = color;
        }
    }

    public void ClearLines()
    {
        lineRenderer.positionCount = 0;

        foreach(var borderLine in borderLineRenderers)
        {
            borderLine.positionCount = 0;
        }
    }

    public void DrawLine(List<Vector3> linePositions, List<Vector3> borderPositions)
    {
        lineRenderer.positionCount = linePositions.Count;

        for(int i = 0; i < linePositions.Count; i++)
        {
            lineRenderer.SetPosition(i, linePositions[i]);
        }

        DrawBorders(borderPositions);
    }

    private void DrawBorders(List<Vector3> borderPositions)
    {
        int currentIteration = 0;

        foreach (var borderLine in borderLineRenderers)
        {
            borderLine.positionCount = borderPositions.Count / 2;

            for(int i = 0; i < borderLine.positionCount; i++, currentIteration++)
            {
                borderLine.SetPosition(i, borderPositions[currentIteration]);
            }
        }

        for(int i = 0; i < borderLineRenderers.Length; i++)
        {
            borderLineRenderers[i].positionCount = borderPositions.Count / 2;

            if (i == 0)
            {
                for(int j = 0; j < borderPositions.Count / 2; j++)
                {
                    borderLineRenderers[i].SetPosition(j, borderPositions[j]);
                }
            }
            else
            {
                for (int j = borderPositions.Count / 2; j < borderPositions.Count; j++)
                {
                    borderLineRenderers[i].SetPosition(j - borderPositions.Count / 2, borderPositions[j]);
                }
            }
        }
    }
}
