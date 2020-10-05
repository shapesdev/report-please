using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GraphicManager : MonoBehaviour, IManager
{
    GraphicRaycaster graphicRaycaster;
    PointerEventData pointerEvent;
    EventSystem eventSystem;

    private GameObject selectedGO;
    private Card card;

    private bool selected = false;
    private bool offsetSet = false;
    private Vector3 offset;

    [SerializeField]
    private RectTransform leftPanel;

    public void Initialize()
    {
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    public void ManagerUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            pointerEvent = new PointerEventData(eventSystem);
            pointerEvent.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            graphicRaycaster.Raycast(pointerEvent, results);

            if(results.Count > 0)
            {
                BringGraphicToFront(results[0].gameObject);
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            selected = false;
            offsetSet = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void FixedManagerUpdate()
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
            card.Check(leftPanel.rect.width, ReportType.PackageBug);
            // SOME SCENARIOMANAGER SHOULD TAKE CARE FROM HERE
        }
    }

    private void BringGraphicToFront(GameObject go)
    {
        selectedGO = go;
        selectedGO.transform.SetSiblingIndex(transform.childCount - 2);

        card = selectedGO.gameObject.GetComponent<Card>();
        selected = true;
    }
}
