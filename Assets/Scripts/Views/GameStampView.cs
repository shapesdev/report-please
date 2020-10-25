using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameStampView : MonoBehaviour, IGameStampView
{
    [SerializeField]
    private GameObject stampPanel;

    public event EventHandler<CanBeReturnedEventArgs> OnReturned = (sender, e) => { };
    public event EventHandler<StampPressEventArgs> OnStampPressed = (sender, e) => { };

    public void StampPress(Sprite sprite)
    {
        var eventArgs = new StampPressEventArgs(sprite);
        OnStampPressed(this, eventArgs);
    }

    private bool StampCanBePlaced(GameObject selectedGO, Vector3 pos)
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

    public void PlaceStamp(GameObject selectedGameObject, Sprite sprite)
    {
        var currentButton = EventSystem.current.currentSelectedGameObject;

        if (StampCanBePlaced(selectedGameObject, currentButton.transform.position) == true)
        {
            var test = GetComponentInChildren<StampTest>();

            if (selectedGameObject.transform.childCount > 1 && test == null)
            {
                var stamp = new GameObject("Stamp");
                stamp.AddComponent<Image>();
                stamp.AddComponent<StampTest>();

                var stampImage = stamp.GetComponent<Image>();
                stampImage.sprite = sprite;
                stampImage.raycastTarget = false;

                for (int i = 0; i < selectedGameObject.transform.childCount; i++)
                {
                    if (selectedGameObject.transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        stamp.transform.SetParent(selectedGameObject.transform.GetChild(i).transform);
                    }
                }

                stamp.transform.position = currentButton.transform.position;
                stamp.transform.localScale = Vector3.one;
                stamp.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);

                var eventArgs = new CanBeReturnedEventArgs(true);
                OnReturned(this, eventArgs);
            }
        }
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

        var eventArgs = new CanBeReturnedEventArgs(false);
        OnReturned(this, eventArgs);

        var stamp = GetComponentInChildren<StampTest>(true);
        Destroy(stamp?.gameObject);
    }

    public void ChangeMode(bool value)
    {
        if (value == true)
        {
            stampPanel.GetComponent<Image>().raycastTarget = false;

            foreach (Transform child in stampPanel.transform)
            {
                child.GetComponent<Image>().raycastTarget = false;
                child.GetChild(0).gameObject.GetComponent<Text>().raycastTarget = false;
            }
        }
        else
        {
            stampPanel.GetComponent<Image>().raycastTarget = true;

            foreach (Transform child in stampPanel.transform)
            {
                child.GetComponent<Image>().raycastTarget = true;
                child.GetChild(0).gameObject.GetComponent<Text>().raycastTarget = true;
            }
        }
    }
}
