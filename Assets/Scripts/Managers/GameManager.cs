using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager
{
    private DataLoadManager loadManager;
    private Tuple<List<AreasNGrabbags>, Dictionary<int, List<IScenario>>> scenarioData;

    [SerializeField]
    private GraphicManager graphicManager;

    private Card currentCard;
    private float currentPanelWidth;

    private int currentDay = 10;
    private int currentScenario = 0;

    public void Initialize()
    {
        loadManager = new DataLoadManager();
        scenarioData = loadManager.GetAreaAndDayData();

        graphicManager.OnDragRight += GraphicManager_OnDragRight;
        graphicManager.OnPapersReturned += GraphicManager_OnPapersReturned;
    }

    private void GraphicManager_OnPapersReturned(object sender, PapersReturnedEventArgs e)
    {
        if(currentScenario + 1 < scenarioData.Item2[currentDay].Count)
        {
            currentScenario++;
            graphicManager.Reset();
        }
    }

    private void GraphicManager_OnDragRight(object sender, DragRightEventArgs e)
    {
        currentCard = e.card;
        currentPanelWidth = e.panelWidth;

        currentCard.Check(currentPanelWidth, scenarioData.Item2[currentDay][currentScenario]);
    }

    public void FixedManagerUpdate()
    {

    }

    public void ManagerUpdate()
    {

    }
}
