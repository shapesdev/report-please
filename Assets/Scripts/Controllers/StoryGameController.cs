using System;
using UnityEngine;

public class StoryGameController
{
    #region Properties
    private readonly IStoryGameModel model;
    private readonly IStoryGameView view;
    private readonly IGameSelectionView selectionView;
    private readonly IGameStampView stampView;
    private readonly ILineView lineView;
    private readonly IDialogueView dialogueView;
    private readonly ICharacterView characterView;

    private FieldCheckController fieldCheckController;
    private CitationCheckController citationCheckController;

    public static event Action<int> OnGameInitialized;
    public static event Action<int> OnInspectorMode;

    public static event Action OnDiscrepancy;
    public static event Action OnCitation;
    #endregion

    public StoryGameController(IStoryGameModel model, IStoryGameView view,
        IGameSelectionView selectionView, IGameStampView stampView, ILineView lineView,
        IDialogueView dialogueView, ICharacterView characterView)
    {
        this.model = model;
        this.view = view;
        this.selectionView = selectionView;
        this.stampView = stampView;
        this.lineView = lineView;
        this.dialogueView = dialogueView;
        this.characterView = characterView;

        fieldCheckController = new FieldCheckController();
        citationCheckController = new CitationCheckController();

        view.Init(model.CurrentDay, model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario]);
        view.OnMousePressed += View_OnMousePressed;
        view.OnMouseReleased += View_OnMouseReleased;
        view.OnMouseHold += View_OnMouseHold;
        view.OnOffsetSet += SelectionView_OnOffsetSet;
        view.OnSpaceBarPressed += View_OnSpaceBarPressed;
        view.OnOffsetChanged += View_OnOffsetChanged;
        view.OnTabPressed += View_OnTabPressed;
        view.OnStartScenarioShowing += View_OnStartScenarioShowing;
        view.OnExport += View_OnExport;

        model.OnHighlight += Model_OnHighlight;

        selectionView.OnGameObjectSelected += SelectionView_OnGameObjectSelected;
        selectionView.OnOffsetSet += SelectionView_OnOffsetSet;
        selectionView.OnPapersReturned += SelectionView_OnPapersReturned;

        stampView.OnReturned += StampView_OnReturned;
        stampView.OnStampPressed += StampView_OnStampPressed;

        OnGameInitialized?.Invoke(1);
    }

    #region Model Callbacks
    private void Model_OnHighlight(object sender, HighlightEventArgs e)
    {
        lineView.CheckFieldHighlight(e.isHighlight, e.goToHighlight, model.InspectorMode);
    }
    #endregion

    #region SelectionView Callbacks
    private void SelectionView_OnPapersReturned(object sender, PapersReturnedEventArgs e)
    {
        var citation = citationCheckController.CheckForCitations(model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario], model.RuleBook,
            model.CurrentStamp, model.DiscrepancyFound);

        if(citation.Item1 == true)
        {
            OnCitation?.Invoke();
            view.EnableCitation(citation.Item2);
            PlayerData.instance.citationsReceived += 1;

            if(model.DiscrepancyFound == true)
            {
                model.UpdateScenarioData(citation.Item2, 5);
            }
            else
            {
                model.UpdateScenarioData(citation.Item2, 0);
            }
        }
        else
        {
            model.UpdateScenarioData(citation.Item2, 10);
            PlayerData.instance.correctReports += 1;
        }

        ShowNextScenario();
    }

    private void ShowNextScenario()
    {
        if (model.CurrentScenario + 1 < model.DaysWithScenarios[model.CurrentDay].Count)
        {
            model.CurrentScenario += 1;
            stampView.Reset();
            DisplayDataForView();
        }
        else
        {
            view.ShowEndDay(model.CurrentDay.Day, model.CurrentScore, model.MaxScore);
            model.SaveScenarioData();

            if (model.CurrentDay.Day != 14)
            {
                model.CurrentDay = new DateTime(model.CurrentDay.Year, model.CurrentDay.Month, model.CurrentDay.Day + 1);
            }
        }
    }

    private void SelectionView_OnOffsetSet(object sender, OffsetSetEventArgs e) { model.OffsetSet = e.offsetSet; }
    private void SelectionView_OnGameObjectSelected(object sender, GameObjectSelectedEventArgs e)
    {
        model.Selected = e.selected;
    }
    #endregion

    #region StampView Callbacks
    private void StampView_OnStampPressed(object sender, StampPressEventArgs e)
    {
        model.CurrentStamp = e.stampType;
        stampView.PlaceStamp(model.SelectedGameObject, e.sprite);
    }

    private void StampView_OnReturned(object sender, CanBeReturnedEventArgs e) { model.CanBeReturned = e.canBeReturned; }
    #endregion

    #region GameView Callbacks
    
    private void DisplayDataForView()
    {
        view.ShowScenario(model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario],
    model.StoryCharacters[model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario].GetTester().GetId()], model.CurrentScenario + 1,
    model.DaysWithScenarios[model.CurrentDay].Count, selectionView, model.CurrentDay, model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario].GetDiscrepancy());
    }

    private void View_OnExport(object sender, ExportPressedEventArgs e) { DataExportHelper.instance.Export(); }

    private void View_OnStartScenarioShowing(object sender, StartScenarioShowingEventArgs e) { DisplayDataForView(); }

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
        CheckInspectorMode();
    }

    private void CheckInspectorMode()
    {
        model.InspectorMode = !model.InspectorMode;

        if (model.InspectorMode == true)
        {
            view.TurnOnInspectorMode();
            OnInspectorMode?.Invoke(0);
        }
        else if (model.InspectorMode == false)
        {
            view.TurnOffInspectorMode();
            ClearLine(true);
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
                SelectField(go);
            }
        }
    }
    #endregion

    #region Line Renderer Logic
    private void SelectField(GameObject selectedGameObject)
    {
        if (model.FirstSelection == null)
        {
            model.FirstSelection = selectedGameObject;
        }
        else if (model.SecondSelection == null && selectedGameObject != model.FirstSelection)
        {
            model.SecondSelection = selectedGameObject;
            model.AddSelectionEdgesToList();

            lineView.DrawLine(model.GetAllLinePositions(), model.WorldEdgePositions);
            TwoFieldsSelected();
        }
        else if (model.SecondSelection == null && selectedGameObject == model.FirstSelection)
        {
            model.FirstSelection = null;
            ClearLine(false);
        }
        else
        {
            ClearLine(true);
            model.FirstSelection = selectedGameObject;
        }
    }

    private void ClearLine(bool clear)
    {
        if(clear)
        {
            if (model.FirstSelection != null) { model.FirstSelection = null; }
            if (model.SecondSelection != null) { model.SecondSelection = null; }
        }

        model.WorldEdgePositions.Clear();
        lineView.ClearLines();
    }

    private void TwoFieldsSelected()
    {
        var fieldValues = fieldCheckController.CheckFields(model.FirstSelection, model.SecondSelection, model.Discrepancies, model.DaysWithScenarios[model.CurrentDay]
            [model.CurrentScenario].GetDiscrepancy());

        model.DiscrepancyFound = fieldValues.Item2;
        OnDiscrepancy?.Invoke();

        if (fieldValues.Item1 == true && fieldValues.Item2 == true)
        {
            view.DisplayFieldText("Discrepancy found");

            dialogueView.ShowDiscrepancyDialogue(model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario].GetDiscrepancy().GetDialogue().GetInspectorWords(),
                model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario].GetDiscrepancy().GetDialogue().GetTesterWords());

            if (model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario].IsEmployeeIdMissing() == true &&
                model.DaysWithScenarios[model.CurrentDay][model.CurrentScenario].GetTester().GetFullName() != null)
            {
                selectionView.ActivateSelectable(3.5f, 2);
            }
        }
        else if (fieldValues.Item1 == true) { view.DisplayFieldText("Matching Data"); }
        else { view.DisplayFieldText("No correlation"); }
    }

    #endregion
}
