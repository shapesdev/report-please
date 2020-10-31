using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LineController : MonoBehaviour, ILineController
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private LineRenderer[] borderLineRenderers;

    private GameObject firstSelection;
    private GameObject secondSelection;

    private List<Vector3> worldEdgePositions;

    public static event Action<int> OnHighlight;

    public event EventHandler<TwoFieldsSelectedEventArgs> OnTwoFieldsSelected = (sender, e) => { };

    private void Start()
    {
        worldEdgePositions = new List<Vector3>();
    }

    public void SelectField(GameObject selectedGameObject)
    {
        if(firstSelection == null)
        {
            firstSelection = selectedGameObject;
            HighlightField(firstSelection);
        }
        else if(secondSelection == null && selectedGameObject != firstSelection)
        {
            secondSelection = selectedGameObject;
            HighlightField(secondSelection);

            AddGameObjectEdgesToList(firstSelection);
            AddGameObjectEdgesToList(secondSelection);

            DrawLine();
            DrawBorders();

            var eventArgs = new TwoFieldsSelectedEventArgs(firstSelection, secondSelection);
            OnTwoFieldsSelected(this, eventArgs);
        }
        else if(secondSelection == null && selectedGameObject == firstSelection)
        {
            UnHighlightField(firstSelection);
            firstSelection = null;
            ClearLine(false);
        }
        else
        {
            UnHighlightField(firstSelection);
            UnHighlightField(secondSelection);

            firstSelection = selectedGameObject;
            secondSelection = null;
            HighlightField(firstSelection);
            ClearLine(false);
        }
    }

    private void HighlightField(GameObject selection)
    {
        OnHighlight?.Invoke(0);

        var text = selection.GetComponent<TMP_Text>();

        if (text == null)
        {
            var img = selection.GetComponent<Image>();
            img.color = ColorHelper.instance.NormalModeColor;
        }
        else
        {
            text.color = ColorHelper.instance.NormalModeColor;
        }
    }

    private void UnHighlightField(GameObject selection)
    {
        OnHighlight?.Invoke(1);

        var text = selection.GetComponent<TMP_Text>();

        if (text == null)
        {
            var img = selection.GetComponent<Image>();
            img.color = ColorHelper.instance.InspectorModeColor;
        }
        else
        {
            text.color = ColorHelper.instance.InspectorModeColor;
        }
    }

    public void ClearLine(bool clear)
    {
        if(clear)
        {
            if (firstSelection != null) { UnHighlightField(firstSelection); }
            firstSelection = null;
        }

        if(secondSelection != null) { UnHighlightField(secondSelection); }
        secondSelection = null;
        worldEdgePositions.Clear();
        lineRenderer.positionCount = 0;

        foreach(var borderLine in borderLineRenderers)
        {
            borderLine.positionCount = 0;
        }
    }

    private void DrawLine()
    {
        var positions = GetAllLinePositions();

        lineRenderer.positionCount = positions.Count;

        for(int i = 0; i < positions.Count; i++)
        {
            lineRenderer.SetPosition(i, positions[i]);
        }
    }

    private void DrawBorders()
    {
        int currentIteration = 0;

        foreach (var borderLine in borderLineRenderers)
        {
            borderLine.positionCount = worldEdgePositions.Count / 2;

            for(int i = 0; i < borderLine.positionCount; i++, currentIteration++)
            {
                borderLine.SetPosition(i, worldEdgePositions[currentIteration]);
            }
        }

        for(int i = 0; i < borderLineRenderers.Length; i++)
        {
            borderLineRenderers[i].positionCount = worldEdgePositions.Count / 2;

            if (i == 0)
            {
                for(int j = 0; j < worldEdgePositions.Count / 2; j++)
                {
                    borderLineRenderers[i].SetPosition(j, worldEdgePositions[j]);
                }
            }
            else
            {
                for (int j = worldEdgePositions.Count / 2; j < worldEdgePositions.Count; j++)
                {
                    borderLineRenderers[i].SetPosition(j - worldEdgePositions.Count / 2, worldEdgePositions[j]);
                }
            }
        }
    }

    private void AddGameObjectEdgesToList(GameObject go)
    {
        Rect localEdges = go.transform.GetComponent<RectTransform>().rect;

        float leftLocalEdge = go.transform.localPosition.x - localEdges.width / 2;
        Vector3 leftLocalPosition = new Vector3(leftLocalEdge, go.transform.localPosition.y, go.transform.localPosition.z);
        Vector3 leftWorldPosition = go.transform.parent.TransformPoint(leftLocalPosition);

        float rightLocalEdge = go.transform.localPosition.x + localEdges.width / 2;
        Vector3 rightLocalPosition = new Vector3(rightLocalEdge, go.transform.localPosition.y, go.transform.localPosition.z);
        Vector3 rightWorldPosition = go.transform.parent.TransformPoint(rightLocalPosition);

        float bottomLocalEdge = go.transform.localPosition.y - localEdges.height / 2;
        Vector3 bottomLocalPosition = new Vector3(go.transform.localPosition.x, bottomLocalEdge, go.transform.localPosition.z);
        Vector3 bottomWorldPosition = go.transform.parent.TransformPoint(bottomLocalPosition);

        float upLocalEdge = go.transform.localPosition.y + localEdges.height / 2;
        Vector3 upLocalPosition = new Vector3(go.transform.localPosition.x, upLocalEdge, go.transform.localPosition.z);
        Vector3 upWorldPosition = go.transform.parent.TransformPoint(upLocalPosition);

        var topLeftWorldPosition = new Vector3(leftWorldPosition.x, upWorldPosition.y, 0);
        var topRightWorldPosition = new Vector3(rightWorldPosition.x, upWorldPosition.y, 0);
        var botRightWorldPosition = new Vector3(rightWorldPosition.x, bottomWorldPosition.y, 0);
        var botLeftWorldPosition = new Vector3(leftWorldPosition.x, bottomWorldPosition.y, 0);

        worldEdgePositions.Add(leftWorldPosition);
        worldEdgePositions.Add(topLeftWorldPosition);
        worldEdgePositions.Add(upWorldPosition);
        worldEdgePositions.Add(topRightWorldPosition);
        worldEdgePositions.Add(rightWorldPosition);
        worldEdgePositions.Add(botRightWorldPosition);
        worldEdgePositions.Add(bottomWorldPosition);
        worldEdgePositions.Add(botLeftWorldPosition);
        worldEdgePositions.Add(leftWorldPosition);
    }

    private Tuple<Vector3, Vector3> GetStartAndEndPositions()
    {
        List<Vector3> positions = new List<Vector3>();

        double currentDistance = 0;

        for(int i = 0; i < worldEdgePositions.Count / 2; i +=2 )
        {
            for(int j = worldEdgePositions.Count / 2; j < worldEdgePositions.Count; j += 2)
            {
                /*If we want to find the distance between two points in a coordinate plane we use a different formula
                that is based on the Pythagorean Theorem where(x1, y1) and(x2, y2) are the coordinates and d marks the distance:
                d = (x2−x1)2 + (y2−y1)2−−−−−−−−−−−−−−−−−−√*/

                double distance = Math.Sqrt(Math.Pow((worldEdgePositions[j].x - worldEdgePositions[i].x), 2) + Math.Pow((worldEdgePositions[j].y - worldEdgePositions[i].y), 2));

                if(currentDistance == 0)
                {
                    currentDistance = distance;

                    positions.Add(worldEdgePositions[i]);
                    positions.Add(worldEdgePositions[j]);
                }
                else if(distance < currentDistance)
                {
                    currentDistance = distance;

                    positions.Clear();
                    positions.Add(worldEdgePositions[i]);
                    positions.Add(worldEdgePositions[j]);
                }
            }
        }
        return new Tuple<Vector3, Vector3>(positions[0], positions[1]);
    }

    private List<Vector3> GetAllLinePositions()
    {
        List<Vector3> allPositions = new List<Vector3>();
        Tuple<Vector3, Vector3> positions = GetStartAndEndPositions();

        /* Formula for finding the Midpoint between two points */
        Vector3 midPos = new Vector3((positions.Item1.x + positions.Item2.x) / 2, (positions.Item1.y + positions.Item2.y) / 2, 0);

        allPositions.Add(positions.Item1);

        if(Vector3.Distance(positions.Item2, positions.Item1) > 1f)
        {
            allPositions.Add(new Vector3(midPos.x, positions.Item1.y, 0));
            allPositions.Add(midPos);
            allPositions.Add(new Vector3(midPos.x, positions.Item2.y, 0));
        }
        allPositions.Add(positions.Item2);

        return allPositions;
    }
}
