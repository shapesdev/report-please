using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleBookCard : Card
{
    [SerializeField]
    private GameObject homePage;
    [SerializeField]
    private RuleBook ruleBook;
    [SerializeField]
    private BasicRuleDisplayer basicRuleDisplayer;
    [SerializeField]
    private AreasDisplayer areasDisplayer;
    [SerializeField]
    private ReportFieldsDisplayer reportFieldsDisplayer;

    private bool changeSize;

    private void OnEnable()
    {
        areasDisplayer.OnPageBack += AreasDisplayer_OnPageBack;
        basicRuleDisplayer.OnPageBack += AreasDisplayer_OnPageBack;
        reportFieldsDisplayer.OnPageBack += AreasDisplayer_OnPageBack;
    }

    private void AreasDisplayer_OnPageBack(object sender, PageClosedEventArgs e)
    {
        DeactiveAllChildren();
        homePage.SetActive(true);
    }

    public override void ChangeSizeToLeft(IScenario scenario)
    {
        if(changeSize == true)
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = paperLeft;
            changeSize = false;

            DeactiveAllChildren();
        }
    }

    public override void ChangeSizeToRight(IScenario scenario)
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
        gameObject.GetComponent<Image>().raycastTarget = false;

        basicRuleDisplayer.TurnOnInspectorMode();
        areasDisplayer.TurnOnInspectorMode();
        reportFieldsDisplayer.TurnOnInspectorMode();
    }

    public override void TurnOffInspectorMode()
    {
        gameObject.GetComponent<Image>().raycastTarget = true;

        basicRuleDisplayer.TurnOffInspectorMode();
        areasDisplayer.TurnOffInspectorMode();
        reportFieldsDisplayer.TurnOffInspectorMode();
    }
}
