using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class StoryGameView : MonoBehaviour, IStoryGameView
{
    private IGameScenarioView[] gameScenarioViews;
    private IGameGeneralView[] gamegeneralViews;

    private Image[] allImages;
    public RectTransform leftPanel;

    public GameObject topPanel;
    public GameObject nextDayGameObject;
    public GameObject pausePanel;
    public GameObject citationPrefab;

    public TMP_Text dateText;
    public Text introDateText;
    public Text fieldCheckerText;
    private TextMeshProUGUI[] allTexts;

    private PlayableDirector director;
    public AudioSource source;

    public event EventHandler<DragRightEventArgs> OnDragRight = (sender, e) => { };
    public event EventHandler<SpaceBarPressedEventArgs> OnSpaceBarPressed = (sender, e) => { };
    public event EventHandler<TabPressedEventArgs> OnTabPressed = (sender, e) => { };
    public event EventHandler<MousePressedEventArgs> OnMousePressed = (sender, e) => { };
    public event EventHandler<MouseReleasedEventArgs> OnMouseReleased = (sender, e) => { };
    public event EventHandler<MouseHoldEventArgs> OnMouseHold = (sender, e) => { };
    public event EventHandler<OffsetSetEventArgs> OnOffsetSet = (sender, e) => { };
    public event EventHandler<OffsetValueEventArgs> OnOffsetChanged = (sender, e) => { };

    public static event Action<int> OnEndDay;
    public static event Action<bool> OnPause;

    public void Init(DateTime day, IScenario scenario)
    {
        director = GetComponent<PlayableDirector>();

        if (director != null)
        {
            TimelineAsset timelineAsset = (TimelineAsset)director.playableAsset;

            foreach (PlayableBinding output in timelineAsset.outputs)
            {
                if (output.streamName == "Audio Track")
                {
                    director.SetGenericBinding(output.sourceObject, source);
                }
            }
            director.Play();
        }

        gameScenarioViews = GetComponentsInChildren<IGameScenarioView>();
        gamegeneralViews = GetComponentsInChildren<IGameGeneralView>();

        allTexts = GetComponentsInChildren<TextMeshProUGUI>();
        allImages = GetComponentsInChildren<Image>();

        ShowScenario(scenario);
        dateText.text = day.ToString("yyyy/MM/dd");

        dateText.gameObject.transform.SetAsLastSibling();
        topPanel.transform.SetAsLastSibling();
        nextDayGameObject.transform.parent.SetAsLastSibling();

        TextWriterHelper.instance.AddWriter(introDateText, day.ToString("MMMM dd, yyyy"), 0.08f);
    }

    public void DisplayFieldText(string value)
    {
        fieldCheckerText.text = value;
        fieldCheckerText.color = ColorHelper.instance.NormalModeColor;

        Invoke("TurnOffFieldText", 1f);
    }

    public void TurnOffFieldText()
    {
        fieldCheckerText.text = "";
    }

    public void ShowScenario(IScenario scenario)
    {
        foreach (var view in gameScenarioViews)
        {
            view.Init(scenario);
        }
    }

    public void EnableCitation(string citation)
    {
        var go = Instantiate(citationPrefab, transform.GetChild(0).transform);
        go?.transform.SetSiblingIndex(transform.GetChild(0).childCount - 5);
        var citationText = go.GetComponentInChildren<Text>();
        citationText.text = "PROTOCOL VIOLATION\n\n" + citation;
    }

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
        dateText.raycastTarget = value;
        dateText.color = color;

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


    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            OpenClosePausePanel();
        }

        if(nextDayGameObject.activeInHierarchy == false && director.state != PlayState.Playing && pausePanel.activeInHierarchy == false)
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
        if(nextDayGameObject.activeInHierarchy == false && director.state != PlayState.Playing && pausePanel.activeInHierarchy == false)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                var eventArgs = new MouseHoldEventArgs();
                OnMouseHold(this, eventArgs);
            }
        }
    }

    public void OpenCloseStampPanel()
    {
        var eventArgs = new TabPressedEventArgs();
        OnTabPressed(this, eventArgs);
    }

    public void ShowEndDay(int day, int score, int maxScore)
    {
        if(nextDayGameObject.activeInHierarchy == false)
        {
            StartCoroutine(DisplayEndScreen(day, score, maxScore));
        }
    }

    IEnumerator DisplayEndScreen(int day, int score, int maxScore)
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);

            nextDayGameObject.SetActive(true);
            OnEndDay?.Invoke(0);

            yield return new WaitForSeconds(1.5f);

            if (day == 13)
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

            if(day == 13)
            {
                nextDayGameObject.transform.GetChild(2).transform.position = nextDayGameObject.transform.GetChild(1).transform.position;
                nextDayGameObject.transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                nextDayGameObject.transform.GetChild(1).gameObject.SetActive(true);
                nextDayGameObject.transform.GetChild(2).gameObject.SetActive(true);
            }

            break;
        }
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

    public void UpdateGameObjectPosition(Vector3 offset, bool offsetSet, GameObject selectedGO)
    {
        if (selectedGO != null)
        {
            if (offsetSet == false)
            {
                offset = Input.mousePosition - selectedGO.transform.localPosition;

                var offsetValueEventArgs = new OffsetValueEventArgs(offset);
                OnOffsetChanged(this, offsetValueEventArgs);

                var offsetEventArgs = new OffsetSetEventArgs(true);
                OnOffsetSet(this, offsetEventArgs);
            }

            Vector3 mousePos = Vector3.zero;

            if(Input.mousePosition.x > Screen.width - 1200f)
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

            selectedGO.transform.localPosition = mousePos - offset;

            var eventArgs = new DragRightEventArgs(leftPanel.rect.width, selectedGO.GetComponent<GameGeneralView>());
            OnDragRight(this, eventArgs);
        }
    }
}