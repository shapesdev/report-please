using System.Collections.Generic;
using UnityEngine;
using System;

public class StoryGameModel: IStoryGameModel
{
    #region Properties
    private DateTime currentDay;
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
    #endregion

    public StoryGameModel(RuleBookSO ruleBook)
    {
        RuleBook = ruleBook;
        DataInitialization dataInitialization = new DataInitialization();
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
}
