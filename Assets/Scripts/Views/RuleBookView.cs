using System;
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
    [SerializeField]
    private Text[] buttonsTexts;

    public static event Action OnTurnPage;

    public void OnEnable()
    {
        areasDisplayer.OnPageBack += AreasDisplayer_OnPageBack;
        basicRuleDisplayer.OnPageBack += AreasDisplayer_OnPageBack;
        reportFieldsDisplayer.OnPageBack += AreasDisplayer_OnPageBack;
    }

    private void OnDisable()
    {
        areasDisplayer.OnPageBack -= AreasDisplayer_OnPageBack;
        basicRuleDisplayer.OnPageBack -= AreasDisplayer_OnPageBack;
        reportFieldsDisplayer.OnPageBack -= AreasDisplayer_OnPageBack;
    }

    private void AreasDisplayer_OnPageBack(object sender, PageClosedEventArgs e)
    {
        DeactiveAllChildren();
        homePage.SetActive(true);
        OnTurnPage?.Invoke();
    }

    public override void ChangeSizeToLeft()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = paperLeft;
        DeactiveAllChildren();
    }

    public override void ChangeSizeToRight()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = paperRight;
        if(homePage.activeInHierarchy == false)
        {
            homePage.SetActive(true);
        }
    }

    public void OpenBasicRules()
    {
        DeactiveAllChildren();

        for(int i = 0; i < ruleBook.basicRules.Count; i++)
        {
            if(ruleBook.basicRules[i].day == PlayerPrefs.GetInt("CurrentDay"))
            {
                basicRuleDisplayer.DisplayRules(ruleBook.basicRules[i].rules);
            }
        }
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
        SwitchModes(false, ColorHelper.instance.InspectorModeColor);

        basicRuleDisplayer.TurnOnInspectorMode();
        areasDisplayer.TurnOnInspectorMode();
        reportFieldsDisplayer.TurnOnInspectorMode();
    }

    public override void TurnOffInspectorMode()
    {
        SwitchModes(true, ColorHelper.instance.NormalModeColor);

        basicRuleDisplayer.TurnOffInspectorMode();
        areasDisplayer.TurnOffInspectorMode();
        reportFieldsDisplayer.TurnOffInspectorMode();
    }

    private void SwitchModes(bool value, Color color)
    {
        homePageLeft.color = color;

        foreach (var img in objectsWithRaycast)
        {
            img.raycastTarget = value;
            img.color = color;
        }

        foreach (var btnText in buttonsTexts)
        {
            btnText.color = color;
        }

        foreach (var btn in buttons)
        {
            btn.interactable = value;
        }
    }
}
