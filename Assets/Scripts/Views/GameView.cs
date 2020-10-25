using System;
using UnityEngine;
using TMPro;

public class GameView : MonoBehaviour, IGameView
{
    private IGameScenarioView[] gameScenarioViews;
    private IGameGeneralView[] gamegeneralViews;

    [SerializeField]
    private RectTransform leftPanel;
    [SerializeField]
    private GameObject topPanel;
    [SerializeField]
    private TMP_Text dateText;

    public event EventHandler<DragRightEventArgs> OnDragRight = (sender, e) => { };
    public event EventHandler<SpaceBarPressedEventArgs> OnSpaceBarPressed = (sender, e) => { };
    public event EventHandler<MousePressedEventArgs> OnMousePressed = (sender, e) => { };
    public event EventHandler<MouseReleasedEventArgs> OnMouseReleased = (sender, e) => { };
    public event EventHandler<MouseHoldEventArgs> OnMouseHold = (sender, e) => { };
    public event EventHandler<OffsetSetEventArgs> OnOffsetSet = (sender, e) => { };
    public event EventHandler<OffsetValueEventArgs> OnOffsetChanged = (sender, e) => { };

    public void Init(DateTime day, IScenario scenario)
    {
        gameScenarioViews = GetComponentsInChildren<IGameScenarioView>();
        gamegeneralViews = GetComponentsInChildren<IGameGeneralView>();

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
        foreach(var view in gamegeneralViews)
        {
            view.TurnOnInspectorMode();
        }
    }

    public void TurnOffInspectorMode()
    {
        foreach(var view in gamegeneralViews)
        {
            view.TurnOffInspectorMode();
        }
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            var eventArgs = new SpaceBarPressedEventArgs();
            OnSpaceBarPressed(this, eventArgs);
        }

        if(Input.GetMouseButtonDown(0))
        {
            var eventArgs = new MousePressedEventArgs();
            OnMousePressed(this, eventArgs);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            var eventArgs = new MouseReleasedEventArgs();
            OnMouseReleased(this, eventArgs);
        }
    }

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            var eventArgs = new MouseHoldEventArgs();
            OnMouseHold(this, eventArgs);
        }
    }

    public void UpdateGameObjectPosition(Vector3 offset, bool offsetSet, GameObject selectedGO)
    {
        if(selectedGO != null)
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

    /*    public void ManagerUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                var eventArgs = new SpaceBarPressedEventArgs();
                OnSpaceBarPressed(this, eventArgs);

                if (inspectorMode == false)
                {
                    foreach (var card in cards)
                    {
                        card.TurnOffInspectorMode();

                        stampPanel.GetComponent<Image>().raycastTarget = true;
                        returnArea.gameObject.GetComponent<Image>().raycastTarget = true;

                        foreach (Transform child in stampPanel.transform)
                        {
                            child.GetComponent<Image>().raycastTarget = true;
                            child.GetChild(0).gameObject.GetComponent<Text>().raycastTarget = true;
                        }
                    }
                    lineDrawer.ClearLine(true);
                }
                else if (inspectorMode == true)
                {
                    foreach (var card in cards)
                    {
                        card.TurnOnInspectorMode();

                        stampPanel.GetComponent<Image>().raycastTarget = false;
                        returnArea.gameObject.GetComponent<Image>().raycastTarget = false;

                        foreach (Transform child in stampPanel.transform)
                        {
                            child.GetComponent<Image>().raycastTarget = false;
                            child.GetChild(0).gameObject.GetComponent<Text>().raycastTarget = false;
                        }
                    }
                }
            }

            if (inspectorMode == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    pointerEvent = new PointerEventData(eventSystem);
                    pointerEvent.position = Input.mousePosition;

                    List<RaycastResult> results = new List<RaycastResult>();

                    graphicRaycaster.Raycast(pointerEvent, results);

                    if (results.Count > 0 && (results[0].gameObject.tag == "Selectable"))
                    {
                        BringGraphicToFront(results[0].gameObject);
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    selected = false;
                    offsetSet = false;
                    Cursor.lockState = CursorLockMode.None;

                    if (selectedGO != null)
                    {
                        if (PaperCanBeReturned(selectedGO.transform.position))
                        {
                            selectedGO.gameObject.SetActive(false);
                        }
                    }

                    if (selectableGO[0].activeInHierarchy == false && selectableGO[1].activeInHierarchy == false)
                    {
                        var eventArgs = new PapersReturnedEventArgs();
                        OnPapersReturned(this, eventArgs);
                    }
                }
            }
            else if (inspectorMode == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    pointerEvent = new PointerEventData(eventSystem);
                    pointerEvent.position = Input.mousePosition;

                    List<RaycastResult> results = new List<RaycastResult>();

                    graphicRaycaster.Raycast(pointerEvent, results);

                    if (results.Count > 0)
                    {
                        Debug.Log(results[0].gameObject);
                        lineDrawer.SelectField(results[0].gameObject);
                    }
                }
            }
        }

        private bool PaperCanBeReturned(Vector3 pos)
        {
            if (selectedGO != null)
            {
                for (int i = 0; i < selectedGO.transform.childCount; i++)
                {
                    for (int j = 0; j < selectedGO.transform.GetChild(i).transform.childCount; j++)
                    {
                        if (selectedGO.transform.GetChild(i).transform.GetChild(j).name == "Stamp" || canBeReturned)
                        {
                            Vector3[] v = new Vector3[4];
                            returnArea.GetWorldCorners(v);

                            if (pos.x >= v[0].x && pos.x <= v[3].x && pos.y >= v[0].y && pos.y <= v[1].y)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public void ActivateStampPanel()
        {
            // ADD ANIMATION FOR THE STAMP PANEL
            foreach (Transform child in stampPanel.transform)
            {
                if (child.gameObject.activeInHierarchy)
                {
                    child.gameObject.SetActive(false);
                }
                else
                {
                    child.gameObject.SetActive(true);
                }
            }
        }

        public void Reset()
        {
            StartCoroutine(ResetEnumerator());
        }

        IEnumerator ResetEnumerator()
        {
            yield return new WaitForSeconds(2f);

            canBeReturned = false;

            foreach (var go in selectableGO)
            {
                var stamp = go?.GetComponentInChildren<StampTest>(true);
                Destroy(stamp?.gameObject);
                go.SetActive(true);
            }
        }*/
}
