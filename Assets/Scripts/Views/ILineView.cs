using System;
using System.Collections.Generic;
using UnityEngine;

public interface ILineView
{
    void CheckFieldHighlight(bool value, GameObject go, bool inspector);
    void ClearLines();
    void DrawLine(List<Vector3> linePositions, List<Vector3> edgePositions);
}
