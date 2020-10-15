using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleBookCard : Card
{
    public GameObject homePage;

    public RuleBook ruleBook;
    public BasicRuleDisplayer basicRuleDisplayer;
    public AreasDisplayer areasDisplayer;

    private bool changeSize;

    private void OnEnable()
    {
        areasDisplayer.OnPageBack += AreasDisplayer_OnPageBack;
        basicRuleDisplayer.OnPageBack += AreasDisplayer_OnPageBack;
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

    private void DeactiveAllChildren()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
