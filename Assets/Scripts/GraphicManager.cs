using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GraphicManager : MonoBehaviour, IManager
{
    GraphicRaycaster graphicRaycaster;
    PointerEventData pointerEvent;
    EventSystem eventSystem;

    private GameObject selectedGO;
    private bool selected = false;
    private bool offsetSet = false;

    private Vector3 offset;

    public RectTransform leftPanel;
    public RectTransform rightPanel;

    public float sizeChangeOffsetRight;
    public float sizeChangeOffsetLeft;

    private Vector2 employeePaperRight = new Vector2(300, 200);
    private Vector2 employeePaperLeft = new Vector2(100, 50);

    private Vector2 reportPaperRight = new Vector2(500, 650);
    private Vector2 reportPaperLeft = new Vector2(100, 150);

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
                Debug.Log("Hit " + results[0].gameObject.name);
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
            ChangeGraphicSize();
        }
    }

    private void ChangeGraphicSize()
    {
        if(selectedGO.transform.localPosition.x >= -Screen.width / 2 + leftPanel.rect.width + sizeChangeOffsetRight)
        {
            if(selectedGO.gameObject.tag == "Report")
            {
                selectedGO.gameObject.GetComponent<RectTransform>().sizeDelta = reportPaperRight;
            }
            else if(selectedGO.gameObject.tag == "Employee")
            {
                selectedGO.gameObject.GetComponent<RectTransform>().sizeDelta = employeePaperRight;
            }
        }
        else if (selectedGO.transform.localPosition.x <= Screen.width / 2 - leftPanel.rect.width - sizeChangeOffsetLeft)
        {
            if (selectedGO.gameObject.tag == "Report")
            {
                selectedGO.gameObject.GetComponent<RectTransform>().sizeDelta = reportPaperLeft;
            }
            else if (selectedGO.gameObject.tag == "Employee")
            {
                selectedGO.gameObject.GetComponent<RectTransform>().sizeDelta = employeePaperLeft;
            }
        }
    }


    private void BringGraphicToFront(GameObject go)
    {
        selectedGO = go;
        selectedGO.transform.SetSiblingIndex(transform.childCount - 2);
        selected = true;
    }
}
