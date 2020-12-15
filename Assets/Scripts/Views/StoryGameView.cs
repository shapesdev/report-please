using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Playables;
using System.Collections.Generic;

public class StoryGameView : MonoBehaviour, IStoryGameView
{
    #region Properties
    private IGameScenarioView[] gameScenarioViews;
    private List<IGameGeneralView> gamegeneralViews;

    private Image[] allImages;
    public Image papersArea;
    public RectTransform leftPanel;

    public GameObject topPanel;
    public GameObject nextDayGameObject;
    public GameObject pausePanel;
    public GameObject citationPrefab;
    public GameObject startScenariosButton;
    public GameObject inspectorWordsGO;
    public GameObject testerWordsGO;

    public TMP_Text dateText;
    public Text introDateText;
    public Text fieldCheckerText;
    public Text currentScenarioText;
    private TextMeshProUGUI[] allTexts;

    private PlayableDirector introDirector;
    public PlayableDirector characterWalkInDirector;
    public PlayableDirector characterWalkingDirector;
    public PlayableDirector characterWalkOutDirector;
    public AudioSource source;

    public event EventHandler<DragRightEventArgs> OnDragRight = (sender, e) => { };
    public event EventHandler<SpaceBarPressedEventArgs> OnSpaceBarPressed = (sender, e) => { };
    public event EventHandler<TabPressedEventArgs> OnTabPressed = (sender, e) => { };
    public event EventHandler<MousePressedEventArgs> OnMousePressed = (sender, e) => { };
    public event EventHandler<MouseReleasedEventArgs> OnMouseReleased = (sender, e) => { };
    public event EventHandler<MouseHoldEventArgs> OnMouseHold = (sender, e) => { };
    public event EventHandler<OffsetSetEventArgs> OnOffsetSet = (sender, e) => { };
    public event EventHandler<OffsetValueEventArgs> OnOffsetChanged = (sender, e) => { };
    public event EventHandler<StartScenarioShowingEventArgs> OnStartScenarioShowing = (sender, e) => { };
    public event EventHandler<ExportPressedEventArgs> OnExport = (sender, e) => { };

    public static event Action<int> OnEndDay;
    public static event Action<bool> OnPause;
    public static event Action OnAnnounce;
    public static event Action OnTesterSpeak;
    public static event Action OnInspectorSpeak;
    #endregion

    public void Init(DateTime day, IScenario scenario)
    {
        introDirector = GetComponent<PlayableDirector>();
        GameIntroPlayer introPlayer = new GameIntroPlayer(introDirector);

        gamegeneralViews = new List<IGameGeneralView>();
        gameScenarioViews = GetComponentsInChildren<IGameScenarioView>(true);
        var generalViews = GetComponentsInChildren<IGameGeneralView>(true);

        allTexts = GetComponentsInChildren<TextMeshProUGUI>();
        allImages = GetComponentsInChildren<Image>();

        dateText.gameObject.transform.SetAsLastSibling();
        topPanel.transform.SetAsLastSibling();
        nextDayGameObject.transform.parent.SetAsLastSibling();

        dateText.text = day.ToString("yyyy/MM/dd");
        Invoke("EnableScenariosButton", 4f);

        foreach (var view in generalViews) { gamegeneralViews.Add(view); }

        introPlayer.PlayIntro(day, introDateText, source);
    }

    #region Update methods
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OpenClosePausePanel();
        }

        if (nextDayGameObject.activeInHierarchy == false && introDirector.state != PlayState.Playing && pausePanel.activeInHierarchy == false)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                var eventArgs = new SpaceBarPressedEventArgs();
                OnSpaceBarPressed(this, eventArgs);
            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                var eventArgs = new TabPressedEventArgs();
                OnTabPressed(this, eventArgs);
            }
            if (Input.GetMouseButtonDown(0))
            {
                var eventArgs = new MousePressedEventArgs();
                OnMousePressed(this, eventArgs);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                var eventArgs = new MouseReleasedEventArgs();
                OnMouseReleased(this, eventArgs);
            }
        }
    }

    private void FixedUpdate()
    {
        if (nextDayGameObject.activeInHierarchy == false && introDirector.state != PlayState.Playing && pausePanel.activeInHierarchy == false)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                var eventArgs = new MouseHoldEventArgs(leftPanel.rect.width);
                OnMouseHold(this, eventArgs);
            }
        }
    }
    #endregion

    #region Citation display
    public void DisplayFieldText(string value)
    {
        fieldCheckerText.text = value;
        fieldCheckerText.color = ColorHelper.instance.NormalModeColor;
        Invoke("TurnOffFieldText", 1.5f);
    }

    public void TurnOffFieldText()
    { 
        fieldCheckerText.text = "";
    }

    public void EnableCitation(string citation)
    {
        var go = Instantiate(citationPrefab, transform.GetChild(0).transform);
        go?.transform.SetSiblingIndex(transform.GetChild(0).childCount - 5);

        gamegeneralViews.Add(go.GetComponent<IGameGeneralView>());

        var citationText = go.GetComponentInChildren<Text>();
        citationText.text = "PROTOCOL VIOLATION\n\n" + citation;
    }
    #endregion

    #region Dialogue display

    public void ShowDiscrepancyDialogue(string inspectorWords, string testerWords)
    {
        StartCoroutine(DisplayDialogue(2f, inspectorWords, testerWords));
    }

    IEnumerator DisplayDialogue(float delay, string inspector, string tester)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            if(inspector != "")
            {
                inspectorWordsGO.transform.GetChild(0).gameObject.GetComponent<Text>().text = inspector;
                inspectorWordsGO.SetActive(true);
                OnInspectorSpeak?.Invoke();
            }
            yield return new WaitForSeconds(1f);
            testerWordsGO.transform.GetChild(0).gameObject.GetComponent<Text>().text = tester;
            testerWordsGO.SetActive(true);
            OnTesterSpeak?.Invoke();
            yield return new WaitForSeconds(3f);
            inspectorWordsGO.SetActive(false);
            testerWordsGO.SetActive(false);

            break;
        }
    }

    #endregion

    #region Report display
    public void StartShowingScenarios()
    {
        OnAnnounce?.Invoke();
        var eventArgs = new StartScenarioShowingEventArgs();
        OnStartScenarioShowing(this, eventArgs);
    }

    public void ShowScenario(IScenario scenario, Sprite sprite, int current, int last,
        IGameSelectionView selectionView, DateTime day, Discrepancy discrepancy)
    {
        foreach (var view in gameScenarioViews)
        {
            view.Init(scenario);
        }

        if (currentScenarioText.gameObject.activeInHierarchy == false)
        {
            currentScenarioText.gameObject.SetActive(true);
        }

        var currentSprite = characterWalkInDirector.gameObject.GetComponentInChildren<Image>(true).sprite;

        if (currentSprite == null)
        {
            characterWalkInDirector.gameObject.GetComponentInChildren<Image>(true).sprite = sprite;
            characterWalkOutDirector.gameObject.GetComponentInChildren<Image>(true).sprite = sprite;

            StartCoroutine(DisplayCharacter(true, false, selectionView, day, scenario, sprite));
        }
        else if(currentSprite != sprite)
        {
            StartCoroutine(DisplayCharacter(true, true, selectionView, day, scenario, sprite));
            characterWalkInDirector.gameObject.GetComponentInChildren<Image>(true).sprite = sprite;
        }
        else
        {
            StartCoroutine(DisplayCharacter(false, false, selectionView, day, scenario, sprite));
        }

        currentScenarioText.text = "Current Report: " + current + "/" + last;
    }

    IEnumerator DisplayCharacter(bool walkIn, bool walkOut, IGameSelectionView selectionView,
        DateTime day, IScenario scenario, Sprite sprite)
    {
        while(true)
        {
            if(walkIn)
            {
                if(walkOut)
                {
                    characterWalkInDirector.transform.GetChild(0).gameObject.SetActive(false);
                    characterWalkOutDirector.Play();
                    yield return new WaitForSeconds(1.3f);
                    characterWalkOutDirector.gameObject.GetComponentInChildren<Image>(true).sprite = sprite;
                }

                var animator = characterWalkInDirector.gameObject.GetComponentInChildren<Animator>(true);
                if (animator.enabled == false){ animator.enabled = true; }

                characterWalkingDirector.Play();
                yield return new WaitForSeconds(2f);
                characterWalkInDirector.Play();
                yield return new WaitForSeconds(2f);
                animator.enabled = false;

                StartCoroutine(DisplayDialogue(0f, "Reports, Please", scenario.GetTester().GetWalkInWords()));
            }

            if (characterWalkOutDirector.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                characterWalkOutDirector.transform.GetChild(0).gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(2f);

            if(day.Day == 10 || scenario.IsEmployeeIdMissing() == true) { selectionView.ActivateSelectable(0, 1); }
            else { selectionView.ActivateSelectable(0, 2); }

            break;
        }
    }
    #endregion

    #region Inspector Mode
    public void TurnOnInspectorMode()
    {
        SwitchModes(true, ColorHelper.instance.InspectorModeColor);
        foreach (var view in gamegeneralViews)
        {
            view.TurnOnInspectorMode();
        }
    }

    public void TurnOffInspectorMode()
    {
        SwitchModes(false, ColorHelper.instance.NormalModeColor);
        foreach (var view in gamegeneralViews)
        {
            view.TurnOffInspectorMode();
        }
    }

    private void SwitchModes(bool value, Color color)
    {
        dateText.color = color;
        currentScenarioText.color = color;
        characterWalkInDirector.gameObject.GetComponentInChildren<Image>(true).color = color;

        dateText.raycastTarget = value;
        startScenariosButton.GetComponent<Image>().raycastTarget = !value;

        foreach (var img in allImages)
        {
            img.raycastTarget = !value;

            if (img.color.a > 0.8f)
            {
                img.color = color;
            }
        }

        foreach (var txt in allTexts)
        {
            if (txt.color.a > 0.8f)
            {
                txt.color = color;
            }
        }

        papersArea.raycastTarget = value;
    }
    #endregion

    #region Ending display
    public void ShowEndDay(int day, int score, int maxScore)
    {
        if (nextDayGameObject.activeInHierarchy == false)
        {
            StartCoroutine(DisplayEndScreen(day, score, maxScore));
        }
    }

    IEnumerator DisplayEndScreen(int day, int score, int maxScore)
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            nextDayGameObject.SetActive(true);
            OnEndDay?.Invoke(0);

            yield return new WaitForSeconds(1.5f);

            if (day == 14)
            {
                TextWriterHelper.instance.AddWriter(nextDayGameObject.GetComponentInChildren<Text>(), "You finished the last day " + day +
                    "\n\n" + "SCORE: " + score + "/" + maxScore, 0.08f);
            }
            else
            {
                TextWriterHelper.instance.AddWriter(nextDayGameObject.GetComponentInChildren<Text>(), "End of day " + day
                    + "\n\nSCORE: " + score + "/" + maxScore, 0.08f);
            }

            yield return new WaitForSeconds(2f);

            if (day == 14)
            {
                nextDayGameObject.transform.GetChild(2).transform.position = nextDayGameObject.transform.GetChild(1).transform.position;
                nextDayGameObject.transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                nextDayGameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
            nextDayGameObject.transform.GetChild(2).gameObject.SetActive(true);
            break;
        }
    }
    #endregion

    #region Extra methods
    public void Export()
    {
        var eventArgs = new ExportPressedEventArgs();
        OnExport(this, eventArgs);
    }

    public void OpenClosePausePanel()
    {
        if (pausePanel.activeInHierarchy == false)
        {
            pausePanel.SetActive(true);
            OnPause.Invoke(true);
        }
        else
        {
            pausePanel.SetActive(false);
            OnPause.Invoke(false);
        }
    }

    private void EnableScenariosButton() { startScenariosButton.SetActive(true); }

    public void OpenCloseStampPanel()
    {
        var eventArgs = new TabPressedEventArgs();
        OnTabPressed(this, eventArgs);
    }

    public void PlayNextDay()
    {
        App.instance.LoadNextDay();
    }

    public void GoBackToMainMenu()
    {
        OnPause.Invoke(false);
        App.instance.Load();
    }
    #endregion
}