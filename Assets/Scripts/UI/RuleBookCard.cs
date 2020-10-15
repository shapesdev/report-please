using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleBookCard : Card
{
    public GameObject homePage;

    public override void ChangeSizeToLeft(IScenario scenario)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = paperLeft;

        homePage.SetActive(false);
    }

    public override void ChangeSizeToRight(IScenario scenario)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = paperRight;

        homePage.SetActive(true);
    }
}
