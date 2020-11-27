using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class StoryGameController
{
    #region Properties
    private readonly IStoryGameModel model;
    private readonly IStoryGameView view;
    private readonly IGameSelectionView selectionView;
    private readonly IGameStampView stampView;
    private readonly ILineView lineView;

    private FieldCheckController fieldCheckController;
    private CitationCheckController citationCheckController;
    private StatsSerializeHelper statsHelper;

    public static event Action<int> OnGameInitialized;
    public static event Action<int> OnInspectorMode;

    public static event Action OnDiscrepancy;
    public static event Action OnCitation;
    #endregion

    public StoryGameController(IStoryGameModel model, IStoryGameView view, IGameSelectionView selectionView, IGameStampView stampView, ILineView lineView)
    {
        this.model = model;
        this.view = view;
        this.selectionView = selectionView;
        this.stampView = stampView;
        this.lineView = lineView;

        view.Init(model.CurrentDay, model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario]);

        fieldCheckController = new FieldCheckController();
        citationCheckController = new CitationCheckController();
        statsHelper = new StatsSerializeHelper();

        view.OnMousePressed += View_OnMousePressed;
        view.OnMouseReleased += View_OnMouseReleased;
        view.OnMouseHold += View_OnMouseHold;
        view.OnOffsetSet += SelectionView_OnOffsetSet;
        view.OnSpaceBarPressed += View_OnSpaceBarPressed;
        view.OnOffsetChanged += View_OnOffsetChanged;
        view.OnTabPressed += View_OnTabPressed;

        selectionView.OnGameObjectSelected += SelectionView_OnGameObjectSelected;
        selectionView.OnOffsetSet += SelectionView_OnOffsetSet;
        selectionView.OnPapersReturned += SelectionView_OnPapersReturned;

        stampView.OnReturned += StampView_OnReturned;
        stampView.OnStampPressed += StampView_OnStampPressed;

        lineView.OnTwoFieldsSelected += LineView_OnTwoFieldsSelected;

        OnGameInitialized?.Invoke(1);
    }

    #region SelectionView Callbacks
    private void SelectionView_OnPapersReturned(object sender, PapersReturnedEventArgs e)
    {
        var citation = citationCheckController.CheckForCitations(model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario], model.RuleBook,
            model.CurrentStamp);

        int score = 0;

        if(citation.Item1 == true)
        {
            OnCitation?.Invoke();
            view.EnableCitation(citation.Item2);

            if(model.DiscrepancyFound == true) { score = 5; }
        }
        else
        {
            score = 10;
        }

        model.CurrentScore += score;

        statsHelper.AddLevel(new ScenarioStats(model.CurrentDay.ToString("MMMM dd, yyyy"), model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario].GetCaseID(),
            model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario].GetTitle(), citation.Item2, score));

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
            statsHelper.SaveLevels();
            view.ShowEndDay(model.CurrentDay.Day, model.CurrentScore, model.MaxScore);

            if(model.CurrentDay.Day != 13)
            {
                model.CurrentDay = new DateTime(model.CurrentDay.Year, model.CurrentDay.Month, model.CurrentDay.Day + 1);
            }
        }
    }
    private void SelectionView_OnOffsetSet(object sender, OffsetSetEventArgs e) { model.OffsetSet = e.offsetSet; }
    private void SelectionView_OnGameObjectSelected(object sender, GameObjectSelectedEventArgs e) { model.Selected = e.selected; }
    #endregion

    #region StampView Callbacks
    private void StampView_OnStampPressed(object sender, StampPressEventArgs e)
    {
        model.CurrentStamp = e.stampType;
        stampView.PlaceStamp(model.SelectedGameObject, e.sprite);
    }

    private void StampView_OnReturned(object sender, CanBeReturnedEventArgs e) { model.CanBeReturned = e.canBeReturned; }
    #endregion

    #region LineView Callbacks
    private void LineView_OnTwoFieldsSelected(object sender, TwoFieldsSelectedEventArgs e)
    {
        var fieldValues = fieldCheckController.CheckFields(e.firstField, e.secondField, model.Discrepancies, model.DaysWithScenarios[model.CurrentDay]
            [model.CurrentScenario].GetDiscrepancy());

        model.DiscrepancyFound = fieldValues.Item2;
        OnDiscrepancy?.Invoke();

        if (fieldValues.Item1 == true && fieldValues.Item2 == true) { view.DisplayFieldText("Discrepancy found"); }
        else if (fieldValues.Item1 == true) { view.DisplayFieldText("Matching Data"); }
        else { view.DisplayFieldText("No correlation"); }
    }
    #endregion

    #region GameView Callbacks
    private void View_OnTabPressed(object sender, TabPressedEventArgs e) { stampView.ActivateStampPanel(model.InspectorMode); }

    private void View_OnOffsetChanged(object sender, OffsetValueEventArgs e) { model.Offset = e.offset; }

    private void View_OnMouseHold(object sender, MouseHoldEventArgs e)
    {
        model.UpdateSelectedGameObjectPosition(e.width);
    }

    private void View_OnMouseReleased(object sender, MouseReleasedEventArgs e)
    {
        selectionView.UnSelectGameObject(model.SelectedGameObject, model.CanBeReturned, model.InspectorMode);
    }

    private void View_OnSpaceBarPressed(object sender, SpaceBarPressedEventArgs e)
    {
        model.InspectorMode = !model.InspectorMode;

        if (model.InspectorMode == true)
        {
            view.TurnOnInspectorMode();
            OnInspectorMode?.Invoke(0);
        }
        else if(model.InspectorMode == false)
        {
            view.TurnOffInspectorMode();
            lineView.ClearLine(true);          
            OnInspectorMode?.Invoke(1);
            view.TurnOffFieldText();
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

                if (model.SelectedGameObject == go)
                {
                    model.SelectedGameObject = null;
                }
            }
        }
    }
    #endregion
}
