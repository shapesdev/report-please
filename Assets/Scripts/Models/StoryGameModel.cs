using System.Collections.Generic;
using UnityEngine;
using System;

public class StoryGameModel: IStoryGameModel
{
    #region Properties
    private DateTime currentDay;
    private GameObject firstSelection;
    private GameObject secondSelection;

    public event EventHandler<HighlightEventArgs> OnHighlight;

    public Dictionary<DateTime, List<IScenario>> DaysWithScenarios { get; }
    public List<Discrepancy> Discrepancies { get; }
    public GameGeneralView CurrentGeneralView { get; set; }
    public DateTime CurrentDay { get
        {
            return currentDay;
        }
        set
        {
            currentDay = value;
            PlayerPrefs.SetInt("CurrentDay", currentDay.Day);
        }
    }
    public float CurrentPanelWidth { get; set; }
    public int CurrentScenario { get; set; } = 0;
    public bool Selected { get; set; }
    public bool OffsetSet { get; set; }
    public bool CanBeReturned { get; set; }
    public bool InspectorMode { get; set; }
    public Vector3 Offset { get; set; }
    public GameObject SelectedGameObject { get; set; }
    public RuleBookSO RuleBook { get; }
    public bool DiscrepancyFound { get; set; }
    public Stamp CurrentStamp { get; set; }
    public int CurrentScore { get; set; }
    public int MaxScore { get; set; }
    public GameObject FirstSelection
    {
        get
        {
            return firstSelection;
        }
        set
        {
            if (value != null)
            {
                firstSelection = value;
                var eventArgs = new HighlightEventArgs(true, firstSelection);
                OnHighlight(this, eventArgs);
            }
            else
            {
                var eventArgs = new HighlightEventArgs(false, firstSelection);
                OnHighlight(this, eventArgs);
                firstSelection = value;
            }
        }
    }
    public GameObject SecondSelection
    {
        get
        {
            return secondSelection;
        }
        set
        {
            if (value != null)
            {
                secondSelection = value;
                var eventArgs = new HighlightEventArgs(true, secondSelection);
                OnHighlight(this, eventArgs);
            }
            else
            {
                var eventArgs = new HighlightEventArgs(false, secondSelection);
                OnHighlight(this, eventArgs);
                secondSelection = value;
            }
        }
    }
    public List<Vector3> WorldEdgePositions { get; set; }
    #endregion

    public StoryGameModel(RuleBookSO ruleBook)
    {
        RuleBook = ruleBook;
        DataInitialization dataInitialization = new DataInitialization();
        WorldEdgePositions = new List<Vector3>();

        DaysWithScenarios = dataInitialization.GetDayData();
        Discrepancies = dataInitialization.GetAllDiscrepancies();

        CurrentDay = new DateTime(2020, 11, PlayerPrefs.GetInt("CurrentDay"));
        DiscrepancyFound = false;
        MaxScore = DaysWithScenarios[CurrentDay].Count * 10;
    }

    public void UpdateSelectedGameObjectPosition(float width)
    {
        CurrentPanelWidth = width;

        if (InspectorMode == false && Selected == true)
        {
            if (SelectedGameObject != null)
            {
                if (OffsetSet == false)
                {
                    Offset = Input.mousePosition - SelectedGameObject.transform.localPosition;
                    OffsetSet = true;
                    CurrentGeneralView = SelectedGameObject.GetComponent<GameGeneralView>();
/*                    var offsetValueEventArgs = new OffsetValueEventArgs(offset);
                    OnOffsetChanged(this, offsetValueEventArgs);

                    var offsetEventArgs = new OffsetSetEventArgs(true);
                    OnOffsetSet(this, offsetEventArgs);*/
                }

                Vector3 mousePos = Vector3.zero;

                if (Input.mousePosition.x > Screen.width - 1200f)
                {
                    var yMax = Screen.height - 300f;
                    var xMax = Screen.width;

                    mousePos.y = Mathf.Clamp(Input.mousePosition.y, 0f, yMax);
                    mousePos.x = Mathf.Clamp(Input.mousePosition.x, 0f, xMax);
                }
                else
                {
                    var yMax = Screen.height - 300f;
                    var yMin = 200f;
                    var xMax = Screen.width;

                    mousePos.y = Mathf.Clamp(Input.mousePosition.y, yMin, yMax);
                    mousePos.x = Mathf.Clamp(Input.mousePosition.x, 0f, xMax);
                }

                SelectedGameObject.transform.localPosition = mousePos - Offset;

                if(CurrentGeneralView != null)
                {
                    CurrentGeneralView.Check(CurrentPanelWidth);
                }
            }
        }
    }

    public void AddSelectionEdgesToList()
    {
        for(int i = 0; i < 2; i++)
        {
            GameObject go;
            if(i == 0) { go = FirstSelection; }
            else { go = SecondSelection; }

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

            WorldEdgePositions.Add(leftWorldPosition);
            WorldEdgePositions.Add(topLeftWorldPosition);
            WorldEdgePositions.Add(upWorldPosition);
            WorldEdgePositions.Add(topRightWorldPosition);
            WorldEdgePositions.Add(rightWorldPosition);
            WorldEdgePositions.Add(botRightWorldPosition);
            WorldEdgePositions.Add(bottomWorldPosition);
            WorldEdgePositions.Add(botLeftWorldPosition);
            WorldEdgePositions.Add(leftWorldPosition);
        }
    }

    private Tuple<Vector3, Vector3> GetStartAndEndPositions()
    {
        List<Vector3> positions = new List<Vector3>();

        double currentDistance = 0;

        for (int i = 0; i < WorldEdgePositions.Count / 2; i += 2)
        {
            for (int j = WorldEdgePositions.Count / 2; j < WorldEdgePositions.Count; j += 2)
            {
                /*If we want to find the distance between two points in a coordinate plane we use a different formula
                that is based on the Pythagorean Theorem where(x1, y1) and(x2, y2) are the coordinates and d marks the distance:
                d = (x2−x1)2 + (y2−y1)2−−−−−−−−−−−−−−−−−−√*/

                double distance = Math.Sqrt(Math.Pow((WorldEdgePositions[j].x - WorldEdgePositions[i].x), 2) + Math.Pow((WorldEdgePositions[j].y - WorldEdgePositions[i].y), 2));

                if (currentDistance == 0)
                {
                    currentDistance = distance;

                    positions.Add(WorldEdgePositions[i]);
                    positions.Add(WorldEdgePositions[j]);
                }
                else if (distance < currentDistance)
                {
                    currentDistance = distance;

                    positions.Clear();
                    positions.Add(WorldEdgePositions[i]);
                    positions.Add(WorldEdgePositions[j]);
                }
            }
        }
        return new Tuple<Vector3, Vector3>(positions[0], positions[1]);
    }

    public List<Vector3> GetAllLinePositions()
    {
        List<Vector3> allPositions = new List<Vector3>();
        Tuple<Vector3, Vector3> positions = GetStartAndEndPositions();

        /* Formula for finding the Midpoint between two points */
        Vector3 midPos = new Vector3((positions.Item1.x + positions.Item2.x) / 2, (positions.Item1.y + positions.Item2.y) / 2, 0);

        allPositions.Add(positions.Item1);
        allPositions.Add(new Vector3(midPos.x, positions.Item1.y, 0));
        allPositions.Add(midPos);
        allPositions.Add(new Vector3(midPos.x, positions.Item2.y, 0));
        allPositions.Add(positions.Item2);

        return allPositions;
    }
}
