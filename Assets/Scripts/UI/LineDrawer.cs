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

    private List<Vector3> worldEdgePositions = new List<Vector3>();

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
            ClearLine(false);
        }
    }

    public void ClearLine(bool clear)
    {
        if(clear)
        {
            firstSelection = null;
        }

        secondSelection = null;
        worldEdgePositions.Clear();
        lineRenderer.positionCount = 0;
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

        worldEdgePositions.Add(leftWorldPosition);
        worldEdgePositions.Add(rightWorldPosition);
        worldEdgePositions.Add(bottomWorldPosition);
        worldEdgePositions.Add(upWorldPosition);
    }

    private Tuple<Vector3, Vector3> GetStartAndEndPositions()
    {
        List<Vector3> positions = new List<Vector3>();

        double currentDistance = 0;

        for(int i = 0; i < worldEdgePositions.Count / 2; i++)
        {
            for(int j = worldEdgePositions.Count / 2; j < worldEdgePositions.Count; j++)
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

        Vector3 startPosition = positions.Item1;
        Vector3 endPosition = positions.Item2;

        allPositions.Add(startPosition);
        allPositions.Add(endPosition);

        return allPositions;
    }
}
