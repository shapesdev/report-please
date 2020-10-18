using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    private GameObject firstSelection;
    private GameObject secondSelection;

    private List<Vector3> firstSelectionEdges;
    private List<Vector3> secondSelectionEdges;
    private List<Vector3> allPositions;

    public void SelectField(GameObject selectedGO)
    {
        if(firstSelection == null)
        {
            firstSelection = selectedGO;
        }
        else if(secondSelection == null)
        {
            secondSelection = selectedGO;

            firstSelectionEdges = new List<Vector3>();
            secondSelectionEdges = new List<Vector3>();

            Debug.Log("I'm here");

            UpdateFirstSelectionEdges();
            UpdateSecondSelectionEdges();

            var positions = GetTwoShortestPositions();

            Debug.Log(positions.Item1);
            Debug.Log(positions.Item2);

            lineRenderer.SetPosition(0, positions.Item1);
            lineRenderer.SetPosition(1, positions.Item2);
        }
        else
        {
            firstSelection = selectedGO;
            secondSelection = null;
        }
    }

    private void UpdateFirstSelectionEdges()
    {
        var localEdges = firstSelection.transform.parent.parent.GetComponent<RectTransform>().rect;

        var localEdgeXAxis1 = firstSelection.transform.localPosition.x - localEdges.width / 2;
        var localStartPos1 = new Vector3(localEdgeXAxis1, firstSelection.transform.localPosition.y, firstSelection.transform.localPosition.z);
        var worldEdge1 = transform.TransformPoint(localStartPos1);

        var localEdgeXAxis2 = firstSelection.transform.localPosition.x + localEdges.width / 2;
        var localStartPos2 = new Vector3(localEdgeXAxis2, firstSelection.transform.localPosition.y, firstSelection.transform.localPosition.z);
        var worldEdge2 = transform.TransformPoint(localStartPos2);

        var localEdgeYAxis1 = firstSelection.transform.localPosition.y - localEdges.height / 2;
        var localStartPos3 = new Vector3(firstSelection.transform.localPosition.x, localEdgeYAxis1, firstSelection.transform.localPosition.z);
        var worldEdge3 = transform.TransformPoint(localStartPos3);

        var localEdgeYAxis2 = firstSelection.transform.localPosition.y + localEdges.height / 2;
        var localStartPos4 = new Vector3(firstSelection.transform.localPosition.x, localEdgeYAxis2, firstSelection.transform.localPosition.z);
        var worldEdge4 = transform.TransformPoint(localStartPos4);

        firstSelectionEdges.Add(worldEdge1);
        firstSelectionEdges.Add(worldEdge2);
        firstSelectionEdges.Add(worldEdge3);
        firstSelectionEdges.Add(worldEdge4);
    }

    private void UpdateSecondSelectionEdges()
    {
        var localEdges = firstSelection.transform.parent.parent.GetComponent<RectTransform>().rect;

        var localEdgeXAxis1 = secondSelection.transform.localPosition.x - localEdges.width / 2;
        var localStartPos1 = new Vector3(localEdgeXAxis1, secondSelection.transform.localPosition.y, secondSelection.transform.localPosition.z);
        var worldEdge1 = transform.TransformPoint(localStartPos1);

        var localEdgeXAxis2 = secondSelection.transform.localPosition.x + localEdges.width / 2;
        var localStartPos2 = new Vector3(localEdgeXAxis2, secondSelection.transform.localPosition.y, secondSelection.transform.localPosition.z);
        var worldEdge2 = transform.TransformPoint(localStartPos2);

        var localEdgeYAxis1 = secondSelection.transform.localPosition.y - localEdges.height / 2;
        var localStartPos3 = new Vector3(secondSelection.transform.localPosition.x, localEdgeYAxis1, secondSelection.transform.localPosition.z);
        var worldEdge3 = transform.TransformPoint(localStartPos3);

        var localEdgeYAxis2 = secondSelection.transform.localPosition.y + localEdges.height / 2;
        var localStartPos4 = new Vector3(secondSelection.transform.localPosition.x, localEdgeYAxis2, secondSelection.transform.localPosition.z);
        var worldEdge4 = transform.TransformPoint(localStartPos4);

        secondSelectionEdges.Add(worldEdge1);
        secondSelectionEdges.Add(worldEdge2);
        secondSelectionEdges.Add(worldEdge3);
        secondSelectionEdges.Add(worldEdge4);
    }

    private Tuple<Vector3, Vector3> GetTwoShortestPositions()
    {
        List<Vector3> shortestPositions = new List<Vector3>();

        float currentShortest = 0f;

        foreach(var first in firstSelectionEdges)
        {
            foreach(var second in secondSelectionEdges)
            {
                float dist = Vector3.Distance(first, second);

                if(currentShortest == 0f)
                {
                    currentShortest = dist;

                    shortestPositions.Add(first);
                    shortestPositions.Add(second);
                }
                else if(dist < currentShortest)
                {
                    currentShortest = dist;

                    shortestPositions.Clear();
                    shortestPositions.Add(first);
                    shortestPositions.Add(second);
                }
            }
        }

        return new Tuple<Vector3, Vector3>(shortestPositions[0], shortestPositions[1]);
    }
}
