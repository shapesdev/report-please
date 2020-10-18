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

    private List<Vector3> allWorldPositions = new List<Vector3>();

    public void SelectField(GameObject selectedGameObject)
    {
        if(firstSelection == null)
        {
            firstSelection = selectedGameObject;
        }
        else if(secondSelection == null)
        {
            secondSelection = selectedGameObject;

            AddGameObjectEdgesToList(firstSelection);
            AddGameObjectEdgesToList(secondSelection);

            DrawLine();
        }
        else
        {
            firstSelection = selectedGameObject;
            secondSelection = null;
            allWorldPositions.Clear();
            ClearLine();
        }
    }

    public void ClearLine()
    {
        lineRenderer.positionCount = 0;
    }

    private void DrawLine()
    {
        var positions = GetPositions();

        lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, positions.Item1);
        lineRenderer.SetPosition(1, positions.Item2);
    }

    private void AddGameObjectEdgesToList(GameObject go)
    {
        var localEdges = go.transform.GetComponent<RectTransform>().rect;

        var leftLocalEdge = go.transform.localPosition.x - localEdges.width / 2;
        var leftLocalPosition = new Vector3(leftLocalEdge, go.transform.localPosition.y, go.transform.localPosition.z);
        var leftWorldPosition = go.transform.parent.TransformPoint(leftLocalPosition);

        var rightLocalEdge = go.transform.localPosition.x + localEdges.width / 2;
        var rightLocalPosition = new Vector3(rightLocalEdge, go.transform.localPosition.y, go.transform.localPosition.z);
        var rightWorldPosition = go.transform.parent.TransformPoint(rightLocalPosition);

        var bottomLocalEdge = go.transform.localPosition.y - localEdges.height / 2;
        var bottomLocalPosition = new Vector3(go.transform.localPosition.x, bottomLocalEdge, go.transform.localPosition.z);
        var bottomWorldPosition = go.transform.parent.TransformPoint(bottomLocalPosition);

        var upLocalEdge = go.transform.localPosition.y + localEdges.height / 2;
        var upLocalPosition = new Vector3(go.transform.localPosition.x, upLocalEdge, go.transform.localPosition.z);
        var upWorldPosition = go.transform.parent.TransformPoint(upLocalPosition);

        allWorldPositions.Add(leftWorldPosition);
        allWorldPositions.Add(rightWorldPosition);
        allWorldPositions.Add(bottomWorldPosition);
        allWorldPositions.Add(upWorldPosition);
    }

    private Tuple<Vector3, Vector3> GetPositions()
    {
        List<Vector3> positions = new List<Vector3>();

        double currentDistance = 0;

        for(int i = 0; i < allWorldPositions.Count / 2; i++)
        {
            for(int j = allWorldPositions.Count / 2; j < allWorldPositions.Count; j++)
            {
                /*If we want to find the distance between two points in a coordinate plane we use a different formula
                that is based on the Pythagorean Theorem where(x1, y1) and(x2, y2) are the coordinates and d marks the distance:
                d = (x2−x1)2 + (y2−y1)2−−−−−−−−−−−−−−−−−−√*/

                var distance = Math.Sqrt(Math.Pow((allWorldPositions[j].x - allWorldPositions[i].x), 2) + Math.Pow((allWorldPositions[j].y - allWorldPositions[i].y), 2));

                if(currentDistance == 0)
                {
                    currentDistance = distance;

                    positions.Add(allWorldPositions[i]);
                    positions.Add(allWorldPositions[j]);
                }
                else if(distance < currentDistance)
                {
                    currentDistance = distance;

                    positions.Clear();
                    positions.Add(allWorldPositions[i]);
                    positions.Add(allWorldPositions[j]);
                }
            }
        }
        return new Tuple<Vector3, Vector3>(positions[0], positions[1]);
    }
}
