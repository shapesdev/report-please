using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GameView : MonoBehaviour, IGameView
{
    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEvent;
    private EventSystem eventSystem;

    private GameObject selectedGO;
    private GameObject stamp;

    [SerializeField]
    private Card[] cards;
    [SerializeField]
    private RectTransform leftPanel;
    [SerializeField]
    private RectTransform returnArea;
    [SerializeField]
    private GameObject[] selectableGO;
    [SerializeField]
    private GameObject stampPanel;
    [SerializeField]
    private TMP_Text dateText;

    [SerializeField]
    private LineManager lineDrawer;

    [SerializeField]
    private InspectorModeView inspectorModeView;
    [SerializeField]
    private NormalModeView normalModeView;

    public bool InspectorMode { set { InspectorMode = value; } }

    public event EventHandler<DragRightEventArgs> OnDragRight = (sender, e) => { };
    public event EventHandler<PapersReturnedEventArgs> OnPapersReturned = (sender, e) => { };
    public event EventHandler<SpaceBarPressedEventArgs> OnSpaceBarPressed = (sender, e) => { };

    public void Init(RuleBookSO ruleBook, DateTime day, IScenario scenario)
    {
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        GetComponent<Canvas>().worldCamera = Camera.main;

        dateText.text = day.ToString("yyyy/MM/dd");

        ShowScenario(scenario);
        // SHOW CURRENT DATE SOMEWHERE
    }

    public void ShowScenario(IScenario scenario)
    {
        // SHOW SCENARIO DATA
    }

    public void ChangeInspectorMode(bool inspectorMode)
    {
        if(inspectorMode == true)
        {
            Debug.Log("Inspector Mode is true");
        }
        else
        {
            StartCoroutine(NormalMode());
            Debug.Log("Inspector mode is false");
        }
    }

    IEnumerator NormalMode()
    {
        while (true)
        {
            yield return null;


        }
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            var eventArgs = new SpaceBarPressedEventArgs();
            OnSpaceBarPressed(this, eventArgs);
        }
    }

    private void FixedUpdate()
    {
        if(selectedGO != null)
      

        if (!offsetSet)
        {
            var offset = position - selectedGO.transform.localPosition;
            offsetSet = !offsetSet;
            Cursor.lockState = CursorLockMode.Confined;
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
        var eventArgs = new DragRightEventArgs(leftPanel.rect.width, selectedGO.GetComponent<Card>());
        OnDragRight(this, eventArgs);
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

        public void FixedManagerUpdate()
        {
            if (inspectorMode == false)
            {
                if (Input.GetKey(KeyCode.Mouse0) && selected)
                {
                    if (!offsetSet)
                    {
                        offset = Input.mousePosition - selectedGO.transform.localPosition;
                        offsetSet = !offsetSet;
                        Cursor.lockState = CursorLockMode.Confined;
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
                    var eventArgs = new DragRightEventArgs(leftPanel.rect.width, selectedGO.GetComponent<Card>());
                    OnDragRight(this, eventArgs);
                }
            }
        }

        private void BringGraphicToFront(GameObject go)
        {
            selectedGO = go;
            selectedGO.transform.SetSiblingIndex(transform.childCount - 3);
            selected = true;
        }

        public void StampEffect(Sprite sprite)
        {
            var currentButton = EventSystem.current.currentSelectedGameObject;

            if (StampCanBePlaced(currentButton.transform.position) == true)
            {
                if (stamp == null && selectedGO.transform.childCount > 1)
                {
                    stamp = new GameObject("Stamp");
                    stamp.AddComponent<Image>();
                    stamp.AddComponent<StampTest>();

                    var stampImage = stamp.GetComponent<Image>();
                    stampImage.sprite = sprite;
                    stampImage.raycastTarget = false;

                    for (int i = 0; i < selectedGO.transform.childCount; i++)
                    {
                        if (selectedGO.transform.GetChild(i).gameObject.activeInHierarchy)
                        {
                            stamp.transform.SetParent(selectedGO.transform.GetChild(i).transform);
                        }
                    }

                    stamp.transform.position = currentButton.transform.position;
                    stamp.transform.localScale = Vector3.one;
                    stamp.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
                    canBeReturned = true;
                }
            }
        }

        private bool StampCanBePlaced(Vector3 pos)
        {
            if (selectedGO != null)
            {
                for (int i = 0; i < selectedGO.transform.childCount; i++)
                {
                    for (int j = 0; j < selectedGO.transform.GetChild(i).transform.childCount; j++)
                    {
                        if (selectedGO.transform.GetChild(i).transform.GetChild(j).tag == "StampArea")
                        {
                            var child = selectedGO.transform.GetChild(i).transform.GetChild(j);

                            Vector3[] v = new Vector3[4];
                            child.GetComponent<RectTransform>().GetWorldCorners(v);

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
