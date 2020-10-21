using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager
{
    private DataLoader dataLoader;
    private DataChecker dataChecker;
    private Dictionary<int, List<IScenario>> dayData;

    [SerializeField]
    private GraphicManager graphicManager;
    [SerializeField]
    private LineManager lineManager;

    private Card currentCard;
    private float currentPanelWidth;

    private int currentDay = 10;
    private int currentScenario = 0;

    public void Initialize()
    {
        dataLoader = new DataLoader();
        dataChecker = new DataChecker();

        dayData = dataLoader.GetAreaAndDayData();
        dataChecker.SetScenario(dayData[currentDay][currentScenario]);

        graphicManager.OnDragRight += GraphicManager_OnDragRight;
        graphicManager.OnPapersReturned += GraphicManager_OnPapersReturned;

        lineManager.OnTwoFieldsSelected += LineManager_OnTwoFieldsSelected;
    }

    private void LineManager_OnTwoFieldsSelected(object sender, TwoFieldsSelectedEventArgs e)
    {
        dataChecker.DoFieldsHaveCorrelation(e.firstField, e.secondField);
    }

    private void GraphicManager_OnPapersReturned(object sender, PapersReturnedEventArgs e)
    {
        if(currentScenario + 1 < dayData[currentDay].Count)
        {
            var citation = dataChecker.CheckForCitations();
            Debug.Log(citation.Item2);

            currentScenario++;
            graphicManager.Reset();
            dataChecker.SetScenario(dayData[currentDay][currentScenario]);
        }
    }

    private void GraphicManager_OnDragRight(object sender, DragRightEventArgs e)
    {
        currentCard = e.card;
        currentPanelWidth = e.panelWidth;

        currentCard.Check(currentPanelWidth, dayData[currentDay][currentScenario]);
    }

    public void FixedManagerUpdate()
    {

    }

    public void ManagerUpdate()
    {

    }
}
