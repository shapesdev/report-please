using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GameController
{
    private readonly IGameModel model;
    private readonly IGameView view;
    private readonly IGameSelectionView selectionView;
    private readonly IGameStampView stampView;
    private readonly ILineController lineController;

    private FieldCheckController fieldCheckController;
    private CitationCheckController citationCheckController;

    public static event Action<int> OnGameInitialized;
    public static event Action<int> OnInspectorMode;

    public static event Action OnDiscrepancy;
    public static event Action OnCitation;

    public GameController(IGameModel model, IGameView view, IGameSelectionView selectionView, IGameStampView stampView, ILineController lineController)
    {
        this.model = model;
        this.view = view;
        this.selectionView = selectionView;
        this.stampView = stampView;
        this.lineController = lineController;

        view.Init(model.CurrentDay, model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario]);

        fieldCheckController = new FieldCheckController();
        citationCheckController = new CitationCheckController();

        model.DiscrepancyFound = false;

        view.OnMousePressed += View_OnMousePressed;
        view.OnMouseReleased += View_OnMouseReleased;
        view.OnMouseHold += View_OnMouseHold;
        view.OnDragRight += View_OnDragRight;
        view.OnOffsetSet += SelectionView_OnOffsetSet;
        view.OnSpaceBarPressed += View_OnSpaceBarPressed;
        view.OnOffsetChanged += View_OnOffsetChanged;
        view.OnTabPressed += View_OnTabPressed;

        selectionView.OnGameObjectSelected += SelectionView_OnGameObjectSelected;
        selectionView.OnOffsetSet += SelectionView_OnOffsetSet;
        selectionView.OnPapersReturned += SelectionView_OnPapersReturned;     

        stampView.OnReturned += StampView_OnReturned;
        stampView.OnStampPressed += StampView_OnStampPressed;

        lineController.OnTwoFieldsSelected += LineView_OnTwoFieldsSelected;

        OnGameInitialized?.Invoke(1);
    }

    private void SelectionView_OnPapersReturned(object sender, PapersReturnedEventArgs e)
    {
        var citation = citationCheckController.CheckForCitations(model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario], model.RuleBook,
            model.DiscrepancyFound, model.CurrentStamp);

        if(citation.Item1 == true)
        {
            OnCitation?.Invoke();
            view.EnableCitation(citation.Item2);
        }

        Debug.Log(citation.Item2);
        model.DiscrepancyFound = false;
        model.CurrentStamp = Stamp.Empty;

        if (model.CurrentScenario + 1 < model.DaysWithScenarios[model.CurrentDay].Count)
        {
            model.CurrentScenario += 1;
            stampView.Reset();
            view.ShowScenario(model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario]);

            selectionView.ActivateSelectable();
        }
        else
        {
            App.instance.Load();
        }
    }

    private void StampView_OnStampPressed(object sender, StampPressEventArgs e)
    {
        model.CurrentStamp = e.stampType;
        stampView.PlaceStamp(model.SelectedGameObject, e.sprite);
    }

    private void StampView_OnReturned(object sender, CanBeReturnedEventArgs e)
    {
        model.CanBeReturned = e.canBeReturned;
    }

    private void View_OnTabPressed(object sender, TabPressedEventArgs e)
    {
        stampView.ActivateStampPanel(model.InspectorMode);
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
        selectionView.UnSelectGameObject(model.SelectedGameObject, model.CanBeReturned, model.InspectorMode);
    }

    private void View_OnSpaceBarPressed(object sender, SpaceBarPressedEventArgs e)
    {
        model.InspectorMode = !model.InspectorMode;
        Debug.Log("INSPECTOR MODE IS:" + model.InspectorMode);

        if (model.InspectorMode == true)
        {
            view.TurnOnInspectorMode();
            OnInspectorMode?.Invoke(0);
        }
        else if(model.InspectorMode == false)
        {
            view.TurnOffInspectorMode();
            lineController.ClearLine(true);
            OnInspectorMode?.Invoke(1);
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
                lineController.SelectField(model.SelectedGameObject);

                if (model.SelectedGameObject == go)
                {
                    model.SelectedGameObject = null;
                }
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
        var values = fieldCheckController.CheckFields(e.firstField, e.secondField, model.Discrepancies, model.DaysWithScenarios[model.CurrentDay]
            [model.CurrentScenario].GetDiscrepancy());

        model.DiscrepancyFound = values.Item2;

        OnDiscrepancy?.Invoke();

        if (values.Item1 == true && values.Item2 == true)
        {
            Debug.Log("Discrepancy is found");
        }
        else if(values.Item1 == true)
        {
            Debug.Log("Matching Data");
        }
        else
        {
            Debug.Log("No Correlation");
        }
    }
}
