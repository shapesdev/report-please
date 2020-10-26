using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    private readonly IGameModel model;
    private readonly IGameView view;
    private readonly IGameSelectionView selectionView;
    private readonly IGameStampView stampView;
    private readonly ILineController lineView;

    private DataCheckController dataCheckController;

    public GameController(IGameModel model, IGameView view, IGameSelectionView selectionView, IGameStampView stampView, ILineController lineView)
    {
        this.model = model;
        this.view = view;
        this.selectionView = selectionView;
        this.stampView = stampView;
        this.lineView = lineView;

        view.Init(model.CurrentDay, model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario]);

        dataCheckController = new DataCheckController();

        view.OnMousePressed += View_OnMousePressed;
        view.OnMouseReleased += View_OnMouseReleased;
        view.OnMouseHold += View_OnMouseHold;
        view.OnDragRight += View_OnDragRight;
        view.OnOffsetSet += SelectionView_OnOffsetSet;
        view.OnSpaceBarPressed += View_OnSpaceBarPressed;
        view.OnOffsetChanged += View_OnOffsetChanged;

        selectionView.OnGameObjectSelected += SelectionView_OnGameObjectSelected;
        selectionView.OnOffsetSet += SelectionView_OnOffsetSet;
        selectionView.OnPapersReturned += SelectionView_OnPapersReturned;     

        stampView.OnReturned += StampView_OnReturned;
        stampView.OnStampPressed += StampView_OnStampPressed;

        lineView.OnTwoFieldsSelected += LineView_OnTwoFieldsSelected;
    }

    private void SelectionView_OnPapersReturned(object sender, PapersReturnedEventArgs e)
    {
        if (model.CurrentScenario + 1 < model.DaysWithScenarios[model.CurrentDay].Count)
        {
            var citation = dataCheckController.CheckForCitations(model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario]);
            Debug.Log(citation.Item2);

            model.CurrentScenario += 1;
            stampView.Reset();
            view.ShowScenario(model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario]);

            selectionView.ActivateSelectable();
        }
    }

    private void StampView_OnStampPressed(object sender, StampPressEventArgs e)
    {
        stampView.PlaceStamp(model.SelectedGameObject, e.sprite);
    }

    private void StampView_OnReturned(object sender, CanBeReturnedEventArgs e)
    {
        model.CanBeReturned = e.canBeReturned;
    }

    private void View_OnOffsetChanged(object sender, OffsetValueEventArgs e)
    {
        model.Offset = e.offset;
    }

    private void View_OnDragRight(object sender, DragRightEventArgs e)
    {
        model.CurrentCard = e.card;
        model.CurrentPanelWidth = e.panelWidth;

        model.CurrentCard.Check(model.CurrentPanelWidth);
    }

    private void View_OnMouseHold(object sender, MouseHoldEventArgs e)
    {
        if(model.InspectorMode == false && model.Selected == true)
        {
            view.UpdateGameObjectPosition(model.Offset, model.OffsetSet, model.SelectedGameObject);
        }
    }

    private void View_OnMouseReleased(object sender, MouseReleasedEventArgs e)
    {
        selectionView.UnSelectGameObject(model.SelectedGameObject, model.CanBeReturned);
    }

    private void View_OnSpaceBarPressed(object sender, SpaceBarPressedEventArgs e)
    {
        model.InspectorMode = !model.InspectorMode;
        Debug.Log("INSPECTOR MODE IS:" + model.InspectorMode);

        if (model.InspectorMode == true)
        {
            view.TurnOnInspectorMode();
        }
        else if(model.InspectorMode == false)
        {
            view.TurnOffInspectorMode();
            lineView.ClearLine(true);
        }
    }

    private void View_OnMousePressed(object sender, MousePressedEventArgs e)
    {
        var go = selectionView.SelectGameObject(model.InspectorMode);

        if(go != null)
        {
            model.SelectedGameObject = go;
            if(model.InspectorMode == true)
            { 
                lineView.SelectField(model.SelectedGameObject);
            }
        }
    }

    private void SelectionView_OnOffsetSet(object sender, OffsetSetEventArgs e)
    {
        model.OffsetSet = e.offsetSet;
    }

    private void SelectionView_OnGameObjectSelected(object sender, GameObjectSelectedEventArgs e)
    {
        model.Selected = e.selected;
    }

    private void LineView_OnTwoFieldsSelected(object sender, TwoFieldsSelectedEventArgs e)
    {
        var values = dataCheckController.CheckFields(e.firstField, e.secondField, model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario]);

        if(values.Item1 == true && values.Item2 == true)
        {
            Debug.Log("There is a discrepancy");
        }
        else if(values.Item1 == true)
        {
            Debug.Log("Matching data");
        }
        else if(values.Item1 == false)
        {
            Debug.Log("No Correlation");
        }
    }
}
