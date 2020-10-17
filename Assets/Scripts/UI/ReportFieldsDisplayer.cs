using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReportFieldsDisplayer : MonoBehaviour
{
    [SerializeField]
    private FieldDisplayer[] fieldDisplayers;
    [SerializeField]
    private GameObject firstPage;
    [SerializeField]
    private GameObject secondPage;

    public event EventHandler<PageClosedEventArgs> OnPageBack = (sender, e) => { };

    public void DisplayReportFields(List<ReportFieldInfo> list)
    {
        gameObject.SetActive(true);

        for (int i = 0; i < fieldDisplayers.Length; i++)
        {
            fieldDisplayers[i].DisplayFields(list[i]);
        }

        firstPage.SetActive(true);
    }

    public void OpenPageTwo()
    {
        firstPage.SetActive(false);
        secondPage.SetActive(true);
    }

    public void GoBackToPageOne()
    {
        firstPage.SetActive(true);
        secondPage.SetActive(false);
    }

    public void DeactiveChildren()
    {
        if(firstPage.activeInHierarchy == true)
        {
            firstPage.SetActive(false);
        }
        if(secondPage.activeInHierarchy == true)
        {
            secondPage.SetActive(false);
        }
    }

    public void GoBackToHomePage()
    {
        firstPage.SetActive(false);

        var eventArgs = new PageClosedEventArgs();
        OnPageBack(this, eventArgs);
    }

    public void TurnOnInspectorMode()
    {
        foreach(var display in fieldDisplayers)
        {
            display.TurnOnInspectorMode();
        }
    }

    public void TurnOffInspectorMode()
    {
        foreach (var display in fieldDisplayers)
        {
            display.TurnOffInspectorMode();
        }
    }
}
