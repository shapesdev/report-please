using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GameView : MonoBehaviour, IGameView
{
    private IGameScenarioView[] gameScenarioViews;
    private IGameGeneralView[] gamegeneralViews;

    private Image[] allImages;
    private TextMeshProUGUI[] allTexts;

    [SerializeField]
    private RectTransform leftPanel;
    [SerializeField]
    private GameObject topPanel;
    [SerializeField]
    private TMP_Text dateText;
    [SerializeField]
    private Text introDateText;
    [SerializeField]
    private Text citationText;
    [SerializeField]
    private GameObject nextDayGameObject;

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
        nextDayGameObject.transform.SetAsLastSibling();

        TextWriterHelper.instance.AddWriter(introDateText, day.ToString("MMMM dd, yyyy"), 0.08f);
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
        citationText.text = "PROTOCOL VIOLATION\n\n" + citation;
        citationText.transform.parent.gameObject.SetActive(true);
        Invoke("DisableCitation", 5f);
    }

    private void DisableCitation()
    {
        citationText.transform.parent.gameObject.SetActive(false);
    }

    public void TurnOnInspectorMode()
    {
        foreach (var view in gamegeneralViews)
        {
            view.TurnOnInspectorMode();
            dateText.raycastTarget = true;
            dateText.color = ColorHelper.instance.InspectorModeColor;
        }

        foreach(var img in allImages)
        {
            if(img.color.a > 0.8f)
            {
                img.color = ColorHelper.instance.InspectorModeColor;
            }
        }

        foreach(var txt in allTexts)
        {
            if(txt.color.a > 0.8f)
            {
                txt.color = ColorHelper.instance.InspectorModeColor;
            }
        }
    }

    public void TurnOffInspectorMode()
    {
        foreach (var view in gamegeneralViews)
        {
            view.TurnOffInspectorMode();
            dateText.raycastTarget = false;
            dateText.color = ColorHelper.instance.NormalModeColor;
        }

        foreach (var img in allImages)
        {
            if(img.color.a > 0.8f)
            {
                img.color = ColorHelper.instance.NormalModeColor;
            }
        }

        foreach (var txt in allTexts)
        {
            if (txt.color.a > 0.8f)
            {
                txt.color = ColorHelper.instance.NormalModeColor;
            }
        }
    }

    private void Update()
    {
        if(nextDayGameObject.activeInHierarchy == false)
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
        if(nextDayGameObject.activeInHierarchy == false)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                var eventArgs = new MouseHoldEventArgs();
                OnMouseHold(this, eventArgs);
            }
        }
    }

    public void ShowEndDay(int day)
    {
        if(nextDayGameObject.activeInHierarchy == false)
        {
            StartCoroutine(DisplayEndScreen(day));
        }
    }

    IEnumerator DisplayEndScreen(int day)
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);

            nextDayGameObject.SetActive(true);
            OnEndDay?.Invoke(0);

            yield return new WaitForSeconds(1.5f);

            if (day == 12)
            {
                TextWriterHelper.instance.AddWriter(nextDayGameObject.GetComponentInChildren<Text>(), "End of day " + day + 
                    "\n" + "You have finished the game", 0.08f);
            }
            else
            {
                TextWriterHelper.instance.AddWriter(nextDayGameObject.GetComponentInChildren<Text>(), "End of day " + day, 0.08f);
            }

            yield return new WaitForSeconds(2f);

            if(day == 12)
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
        App.instance.Load();
    }

    public void UpdateGameObjectPosition(Vector3 offset, bool offsetSet, GameObject selectedGO)
    {
        if (selectedGO != null)
        {
            if (offsetSet == false)
            {
                Cursor.lockState = CursorLockMode.Confined;
                offset = Input.mousePosition - selectedGO.transform.localPosition;

                var offsetValueEventArgs = new OffsetValueEventArgs(offset);
                OnOffsetChanged(this, offsetValueEventArgs);

                var offsetEventArgs = new OffsetSetEventArgs(true);
                OnOffsetSet(this, offsetEventArgs);
            }

            if (Input.mousePosition.y <= Screen.height - 300f && Input.mousePosition.y >= 0f)
            {
#if UNITY_STANDALONE_OSX
        if(Input.mousePosition.x <= Screen.width && Input.mousePosition.x >= 0f)
        {
            selectedGO.transform.localPosition = Input.mousePosition - offset;
        }
#endif

#if UNITY_STANDALONE_WIN
                selectedGO.transform.localPosition = Input.mousePosition - offset;
#endif
            }
            var eventArgs = new DragRightEventArgs(leftPanel.rect.width, selectedGO.GetComponent<GameGeneralView>());
            OnDragRight(this, eventArgs);
        }
    }
}