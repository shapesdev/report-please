using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EmployeeView : GameGeneralView, IGameScenarioView
{
    public TMP_Text fullName;
    public TMP_Text email;
    public TMP_Text dateStarted;
    public TMP_Text dateOfExpiry;

    public TMP_Text[] allTexts;

    public void Init(IScenario scenario)
    {
        var tester = scenario.GetTester();

        fullName.text = tester.GetFullName();
        email.text = tester.GetEmail();
        dateStarted.text = tester.GetStartedDate().ToString("yyyy/MM/dd");
        dateOfExpiry.text = tester.GetExpiryDate().ToString("yyyy/MM/dd");
    }

    public override void ChangeSizeToLeft()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = paperLeft;

        if (transform.GetChild(0).gameObject.activeInHierarchy == true)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public override void ChangeSizeToRight()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = paperRight;

        if (transform.GetChild(0).gameObject.activeInHierarchy == false)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public override void TurnOnInspectorMode()
    {
        gameObject.GetComponent<Image>().raycastTarget = false;
        gameObject.GetComponent<Image>().color = ColorHelper.instance.InspectorModeColor;

        foreach (var txt in allTexts)
        {
            txt.raycastTarget = true;
            txt.color = ColorHelper.instance.InspectorModeColor;
        }
    }

    public override void TurnOffInspectorMode()
    {
        gameObject.GetComponent<Image>().raycastTarget = true;
        gameObject.GetComponent<Image>().color = ColorHelper.instance.NormalModeColor;

        foreach (var txt in allTexts)
        {
            txt.raycastTarget = false;
            txt.color = ColorHelper.instance.NormalModeColor;
        }
    }
}
