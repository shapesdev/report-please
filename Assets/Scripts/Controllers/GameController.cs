using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    private readonly IGameModel model;
    private readonly IGameView view;
    private readonly IGameSelectionView selectionView;

    private readonly LineManager lineManager;

    private DataChecker dataChecker;

    public GameController(IGameModel model, IGameView view, IGameSelectionView selectionView)
    {
        this.model = model;
        this.view = view;
        this.selectionView = selectionView;

        view.Init(model.RuleBook, model.CurrentDay, model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario]);

        view.OnMousePressed += View_OnMousePressed;
        view.OnMouseReleased += View_OnMouseReleased;
        view.OnMouseHold += View_OnMouseHold;
        view.OnDragRight += View_OnDragRight;
        view.OnOffsetSet += SelectionView_OnOffsetSet;
        view.OnOffsetChanged += View_OnOffsetChanged;
        selectionView.OnGameObjectSelected += SelectionView_OnGameObjectSelected;
        selectionView.OnOffsetSet += SelectionView_OnOffsetSet;

        //dataChecker = new DataChecker();
    }

    private void View_OnOffsetChanged(object sender, OffsetValueEventArgs e)
    {
        model.Offset = e.offset;
    }

    private void View_OnDragRight(object sender, DragRightEventArgs e)
    {
        model.CurrentCard = e.card;
        model.CurrentPanelWidth = e.panelWidth;

        model.CurrentCard.Check(model.CurrentPanelWidth, model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario]);
    }

    private void View_OnMouseHold(object sender, MouseHoldEventArgs e)
    {
        view.UpdateGameObjectPosition(model.Offset, model.OffsetSet, model.SelectedGameObject);
    }

    private void SelectionView_OnOffsetSet(object sender, OffsetSetEventArgs e)
    {
        model.OffsetSet = e.offsetSet;
    }

    private void View_OnMouseReleased(object sender, MouseReleasedEventArgs e)
    {
        selectionView.UnSelectGameObject();
        model.SelectedGameObject = null;
    }

    private void View_OnSpaceBarPressed(object sender, SpaceBarPressedEventArgs e)
    {
        model.InspectorMode = !model.InspectorMode;
    }

    private void View_OnMousePressed(object sender, MousePressedEventArgs e)
    {
        model.SelectedGameObject = selectionView.SelectGameObject(model.InspectorMode);
    }

    private void SelectionView_OnGameObjectSelected(object sender, GameObjectSelectedEventArgs e)
    {
        model.Selected = e.selected;
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
}
