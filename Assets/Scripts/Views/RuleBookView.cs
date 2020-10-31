﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleBookView : GameGeneralView, IGameGeneralView
{
    [SerializeField]
    private Image[] objectsWithRaycast;
    [SerializeField]
    private Image homePageLeft;
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private GameObject homePage;
    [SerializeField]
    private RuleBookSO ruleBook;
    [SerializeField]
    private BasicRuleDisplayer basicRuleDisplayer;
    [SerializeField]
    private AreasDisplayer areasDisplayer;
    [SerializeField]
    private ReportFieldsDisplayer reportFieldsDisplayer;

    public static event Action OnTurnPage;

    private bool changeSize;

    public void OnEnable()
    {
        areasDisplayer.OnPageBack += AreasDisplayer_OnPageBack;
        basicRuleDisplayer.OnPageBack += AreasDisplayer_OnPageBack;
        reportFieldsDisplayer.OnPageBack += AreasDisplayer_OnPageBack;
    }

    private void AreasDisplayer_OnPageBack(object sender, PageClosedEventArgs e)
    {
        DeactiveAllChildren();
        homePage.SetActive(true);
        OnTurnPage?.Invoke();
    }

    public override void ChangeSizeToLeft()
    {
        if(changeSize == true)
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = paperLeft;
            changeSize = false;

            DeactiveAllChildren();
        }
    }

    public override void ChangeSizeToRight()
    {
        if(changeSize == false)
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = paperRight;
            changeSize = true;

            homePage.SetActive(true);
        }
    }

    public void OpenBasicRules()
    {
        DeactiveAllChildren();
        basicRuleDisplayer.DisplayRules(ruleBook.basicRules);
        OnTurnPage?.Invoke();
    }

    public void OpenAreas()
    {
        DeactiveAllChildren();
        areasDisplayer.DisplayAreasPageOne(ruleBook.areas);
    }

    public void OpenFieldsInfo()
    {
        DeactiveAllChildren();
        reportFieldsDisplayer.DisplayReportFields(ruleBook.reportFields);
        OnTurnPage?.Invoke();
    }

    private void DeactiveAllChildren()
    {
        reportFieldsDisplayer.DeactiveChildren();
        areasDisplayer.DeactiveChildren();

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public override void TurnOnInspectorMode()
    {
        homePageLeft.color = ColorHelper.instance.InspectorModeColor;

        foreach (var img in objectsWithRaycast)
        {
            img.raycastTarget = false;
            img.color = ColorHelper.instance.InspectorModeColor;
        }

        foreach(var btn in buttons)
        {
            btn.interactable = false;
        }

        basicRuleDisplayer.TurnOnInspectorMode();
        areasDisplayer.TurnOnInspectorMode();
        reportFieldsDisplayer.TurnOnInspectorMode();
    }

    public override void TurnOffInspectorMode()
    {
        homePageLeft.color = ColorHelper.instance.NormalModeColor;

        foreach (var img in objectsWithRaycast)
        {
            img.raycastTarget = true;
            img.color = ColorHelper.instance.NormalModeColor;
        }

        foreach (var btn in buttons)
        {
            btn.interactable = true;
        }

        basicRuleDisplayer.TurnOffInspectorMode();
        areasDisplayer.TurnOffInspectorMode();
        reportFieldsDisplayer.TurnOffInspectorMode();
    }
}
