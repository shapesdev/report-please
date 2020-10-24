using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameSelectionView : MonoBehaviour, IGameSelectionView
{
    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEvent;
    private EventSystem eventSystem;

    public event EventHandler<GameObjectSelectedEventArgs> OnGameObjectSelected = (sender, e) => { };
    public event EventHandler<OffsetSetEventArgs> OnOffsetSet = (sender, e) => { };

    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    public GameObject SelectGameObject(bool inspectorMode)
    {
        pointerEvent = new PointerEventData(eventSystem);
        pointerEvent.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        graphicRaycaster.Raycast(pointerEvent, results);

        if (results.Count > 0 && (results[0].gameObject.tag == "Selectable"))
        {
            if(inspectorMode == false) { BringGameObjectToFront(results[0].gameObject); }
            return results[0].gameObject;
        }
        return null;
    }

    private void BringGameObjectToFront(GameObject go)
    {
        go?.transform.SetSiblingIndex(transform.childCount - 4);

        var eventArgs = new GameObjectSelectedEventArgs(true);
        OnGameObjectSelected(this, eventArgs);
    }

    public void UnSelectGameObject()
    {
        var eventArgs = new GameObjectSelectedEventArgs(false);
        OnGameObjectSelected(this, eventArgs);

        var eventArgs2 = new OffsetSetEventArgs(false);
        OnOffsetSet(this, eventArgs2);

        Cursor.lockState = CursorLockMode.None;

        //This needs another class for this behavior
/*        if (selectedGO != null)
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
        }*/
    }
}
