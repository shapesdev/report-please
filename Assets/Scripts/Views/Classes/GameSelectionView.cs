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

    public GameObject[] selectableGameObjects;
    public RectTransform returnArea;

    private Image returnAreaImage;

    public event EventHandler<GameObjectSelectedEventArgs> OnGameObjectSelected = (sender, e) => { };
    public event EventHandler<OffsetSetEventArgs> OnOffsetSet = (sender, e) => { };
    public event EventHandler<PapersReturnedEventArgs> OnPapersReturned = (sender, e) => { };

    public static event Action<int> OnPaperDrag;

    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        returnAreaImage = returnArea.gameObject.GetComponent<Image>();
    }

    public GameObject SelectGameObject(bool inspectorMode)
    {
        pointerEvent = new PointerEventData(eventSystem);
        List<RaycastResult> results = new List<RaycastResult>();

        pointerEvent.position = Input.mousePosition;
        graphicRaycaster.Raycast(pointerEvent, results);

        if (results.Count > 0)
        {
            if(results[0].gameObject.tag == "Selectable" || results[0].gameObject.tag == "RuleBookSelectable")
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
        go?.transform.SetSiblingIndex(transform.childCount - 5);

        var eventArgs = new GameObjectSelectedEventArgs(true);
        OnGameObjectSelected(this, eventArgs);
    }

    public void UnSelectGameObject(GameObject go, bool canBeReturned, bool inspector)
    {
        var eventArgs = new GameObjectSelectedEventArgs(false);
        OnGameObjectSelected(this, eventArgs);

        var eventArgs2 = new OffsetSetEventArgs(false);
        OnOffsetSet(this, eventArgs2);

        if (go != null)
        {
            Vector3[] v = new Vector3[4];
            returnArea.GetWorldCorners(v);

            if (go.transform.position.x >= v[0].x && go.transform.position.x <= v[3].x
                && go.transform.position.y >= v[0].y && go.transform.position.y <= v[1].y)
            {
                if (PaperCanBeReturned(go.transform.position, go, canBeReturned))
                {
                    if (go.tag == "RuleBookSelectable")
                    {
                        go.transform.localPosition = new Vector3(-600, -320, 0);
                    }
                    else
                    {
                        go.gameObject.SetActive(false);
                    }
                }
                else
                {
                    go.transform.localPosition = new Vector3(-600, -320, 0);
                }
            }
        }

        if (selectableGameObjects[0].activeInHierarchy == false && selectableGameObjects[1].activeInHierarchy == false && canBeReturned)
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
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void ActivateSelectable(float delay, int count)
    {
        if(delay == 0)
        {
            for (int i = 0; i < count; i++)
            {
                if (selectableGameObjects[i].activeInHierarchy == false)
                {
                    selectableGameObjects[i].transform.localPosition = new Vector3(-600, -320, 0);
                    selectableGameObjects[i].SetActive(true);
                }
            }
        }
        else
        {
            StartCoroutine(ActivateSelectables(delay));
        }
    }

    IEnumerator ActivateSelectables(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            for (int i = 0; i < selectableGameObjects.Length; i++)
            {
                if (selectableGameObjects[i].activeInHierarchy == false)
                {
                    selectableGameObjects[i].transform.localPosition = new Vector3(-600, -320, 0);
                    selectableGameObjects[i].SetActive(true);
                }
            }
            break;
        }
    }

    public void ChangeMode(bool value)
    {
        if(value == false) { returnAreaImage.raycastTarget = true; }
        else { returnAreaImage.raycastTarget = false; }
    }
}
