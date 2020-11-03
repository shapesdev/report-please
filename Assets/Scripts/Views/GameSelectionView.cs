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

    [SerializeField]
    private GameObject[] selectableGameObjects;
    [SerializeField]
    private RectTransform returnArea;

    public event EventHandler<GameObjectSelectedEventArgs> OnGameObjectSelected = (sender, e) => { };
    public event EventHandler<OffsetSetEventArgs> OnOffsetSet = (sender, e) => { };
    public event EventHandler<PapersReturnedEventArgs> OnPapersReturned = (sender, e) => { };

    public static event Action<int> OnPaperDrag;

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

        if (results.Count > 0)
        {
            if(results[0].gameObject.tag == "Selectable")
            {
                OnPaperDrag?.Invoke(0);
                BringGameObjectToFront(results[0].gameObject);
                return results[0].gameObject;
            }
            else if(inspectorMode == true)
            {
                return results[0].gameObject;
            }
        }
        return null;
    }

    private void BringGameObjectToFront(GameObject go)
    {
        go?.transform.SetSiblingIndex(transform.childCount - (transform.childCount / 2));

        var eventArgs = new GameObjectSelectedEventArgs(true);
        OnGameObjectSelected(this, eventArgs);
    }

    public void UnSelectGameObject(GameObject go, bool canBeReturned, bool inspector)
    {
        var eventArgs = new GameObjectSelectedEventArgs(false);
        OnGameObjectSelected(this, eventArgs);

        var eventArgs2 = new OffsetSetEventArgs(false);
        OnOffsetSet(this, eventArgs2);

/*        if(inspector == false && go != null)
        {
            OnPaperDrag?.Invoke(1);
        }*/

        Cursor.lockState = CursorLockMode.None;

        if (go != null)
        {
            if (PaperCanBeReturned(go.transform.position, go, canBeReturned))
            {
                go.gameObject.SetActive(false);
            }
        }

        if (selectableGameObjects[0].activeInHierarchy == false && selectableGameObjects[1].activeInHierarchy == false)
        {
            var paperReturnedEventArgs = new PapersReturnedEventArgs();
            OnPapersReturned(this, paperReturnedEventArgs);
        }
    }


    private bool PaperCanBeReturned(Vector3 pos, GameObject selectedGO, bool canBeReturned)
    {
        if (selectedGO != null)
        {
            for (int i = 0; i < selectedGO.transform.childCount; i++)
            {
                for (int j = 0; j < selectedGO.transform.GetChild(i).transform.childCount; j++)
                {
                    if (GetComponentInChildren<StampTest>() != null || canBeReturned)
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

    public void ActivateSelectable()
    {
        Invoke("ActivateSelectableGameObjects", 1.5f);
    }

    private void ActivateSelectableGameObjects()
    {
        foreach (var go in selectableGameObjects)
        {
            go.SetActive(true);
        }
    }

    public void ChangeMode(bool value)
    {
        if(value == false)
        {
            returnArea.gameObject.GetComponent<Image>().raycastTarget = true;
        }
        else
        {
            returnArea.gameObject.GetComponent<Image>().raycastTarget = false;
        }
    }
}
