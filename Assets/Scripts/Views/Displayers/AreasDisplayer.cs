using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AreasDisplayer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] areasTextPage1;
    [SerializeField]
    private TMP_Text[] areasTextPage2;

    [SerializeField]
    private GameObject firstPage;
    [SerializeField]
    private GameObject secondPage;

    private List<AreasNGrabbags> areas;

    public event EventHandler<PageClosedEventArgs> OnPageBack = (sender, e) => { };

    public void DisplayAreasPageOne(List<AreasNGrabbags> areas)
    {
        gameObject.SetActive(true);

        this.areas = areas;

        OpenPageOne();
    }

    public void OpenPageOne()
    {
        if(secondPage.activeInHierarchy)
        {
            secondPage.SetActive(false);
        }

        for (int i = 0; i < areasTextPage1.Length; i++)
        {
            areasTextPage1[i].text = areas[i].area + " - " + areas[i].grabbag;
            areasTextPage1[i].gameObject.GetComponent<FieldData>().SetData(areas[i].area + " " + areas[i].grabbag);
        }

        firstPage.SetActive(true);
    }

    public void OpenPageTwo()
    {
        firstPage.SetActive(false);

        int currentArea = areasTextPage1.Length;

        for (int i = 0; i < areasTextPage2.Length; i++, currentArea++)
        {
            areasTextPage2[i].text = areas[currentArea].area + " - " + areas[currentArea].grabbag;
            areasTextPage2[i].gameObject.GetComponent<FieldData>().SetData(areas[currentArea].area + " " + areas[currentArea].grabbag);
        }

        secondPage.SetActive(true);
    }

    public void DeactiveChildren()
    {
        if (firstPage.activeInHierarchy == true)
        {
            firstPage.SetActive(false);
        }
        if (secondPage.activeInHierarchy == true)
        {
            secondPage.SetActive(false);
        }
    }

    public void OpenHomePage()
    {
        var eventArgs = new PageClosedEventArgs();
        OnPageBack(this, eventArgs);
    }

    public void TurnOnInspectorMode()
    {
        if(firstPage.activeInHierarchy == true)
        {
            foreach (var text in areasTextPage1)
            {
                text.raycastTarget = true;
            }
        }
        else if(secondPage.activeInHierarchy == true)
        {
            foreach (var text in areasTextPage2)
            {
                text.raycastTarget = true;
            }
        }
    }

    public void TurnOffInspectorMode()
    {
        if (firstPage.activeInHierarchy == true)
        {
            foreach (var text in areasTextPage1)
            {
                text.raycastTarget = false;
            }
        }
        else if (secondPage.activeInHierarchy == true)
        {
            foreach (var text in areasTextPage2)
            {
                text.raycastTarget = false;
            }
        }
    }
}
