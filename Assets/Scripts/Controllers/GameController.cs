using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    private readonly IGameModel model;
    private readonly IGameView view;

    private DataChecker dataChecker;

/*    [SerializeField]
    private GraphicManager graphicManager;
    [SerializeField]
    private LineManager lineManager;*/

    public GameController(IGameModel model, IGameView view)
    {
        this.model = model;
        this.view = view;

        view.Init(model.RuleBook, model.CurrentDay, model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario]);

        model.OnInspectorModeActivated += Model_OnInspectorModeActivated;

        //dataChecker = new DataChecker();

/*        //dataChecker.SetScenario(dayData[currentDay][currentScenario]);

        graphicManager.OnDragRight += GraphicManager_OnDragRight;
        graphicManager.OnPapersReturned += GraphicManager_OnPapersReturned;

        lineManager.OnTwoFieldsSelected += LineManager_OnTwoFieldsSelected;*/
    }

    private void Model_OnInspectorModeActivated(object sender, InspectorModeEventArgs e)
    {
        view.ChangeInspectorMode(model.InspectorMode);
    }

    private void View_OnSpaceBarPressed(object sender, SpaceBarPressedEventArgs e)
    {
        model.InspectorMode = !model.InspectorMode;
    }

    private void LineManager_OnTwoFieldsSelected(object sender, TwoFieldsSelectedEventArgs e)
    {
        dataChecker.DoFieldsHaveCorrelation(e.firstField, e.secondField);
    }

    private void GraphicManager_OnPapersReturned(object sender, PapersReturnedEventArgs e)
    {
/*        if (currentScenario + 1 < dayData[currentDay].Count)
        {
            var citation = dataChecker.CheckForCitations();
            Debug.Log(citation.Item2);

            currentScenario++;
            graphicManager.Reset();
            dataChecker.SetScenario(dayData[currentDay][currentScenario]);
        }*/
    }

    private void GraphicManager_OnDragRight(object sender, DragRightEventArgs e)
    {
/*        currentCard = e.card;
        currentPanelWidth = e.panelWidth;

        currentCard.Check(currentPanelWidth, dayData[currentDay][currentScenario]);*/
    }
}
