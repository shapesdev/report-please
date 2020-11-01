using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public event EventHandler<DragRightEventArgs> OnDragRight = (sender, e) => { };
    public event EventHandler<SpaceBarPressedEventArgs> OnSpaceBarPressed = (sender, e) => { };
    public event EventHandler<TabPressedEventArgs> OnTabPressed = (sender, e) => { };
    public event EventHandler<MousePressedEventArgs> OnMousePressed = (sender, e) => { };
    public event EventHandler<MouseReleasedEventArgs> OnMouseReleased = (sender, e) => { };
    public event EventHandler<MouseHoldEventArgs> OnMouseHold = (sender, e) => { };
    public event EventHandler<OffsetSetEventArgs> OnOffsetSet = (sender, e) => { };
    public event EventHandler<OffsetValueEventArgs> OnOffsetChanged = (sender, e) => { };

    public void Init(DateTime day, IScenario scenario)
    {
        gameScenarioViews = GetComponentsInChildren<IGameScenarioView>();
        gamegeneralViews = GetComponentsInChildren<IGameGeneralView>();

        allTexts = GetComponentsInChildren<TextMeshProUGUI>();
        allImages = GetComponentsInChildren<Image>();

        ShowScenario(scenario);
        dateText.text = day.ToString("yyyy/MM/dd");

        dateText.gameObject.transform.SetAsLastSibling();
        topPanel.transform.SetAsLastSibling();
    }

    public void ShowScenario(IScenario scenario)
    {
        foreach (var view in gameScenarioViews)
        {
            view.Init(scenario);
        }
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
        if (Input.GetKeyUp(KeyCode.Space))
        {
            var eventArgs = new SpaceBarPressedEventArgs();
            OnSpaceBarPressed(this, eventArgs);
        }
        if(Input.GetKeyUp(KeyCode.Tab))
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

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            var eventArgs = new MouseHoldEventArgs();
            OnMouseHold(this, eventArgs);
        }
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