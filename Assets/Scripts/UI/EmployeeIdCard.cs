using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EmployeeIdCard : Card
{
    public TMP_Text fullName;
    public TMP_Text email;
    public TMP_Text dateStarted;
    public TMP_Text dateOfExpiry;
    //public Image picture;

    public override void ChangeSizeToLeft(IScenario scenario)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = paperLeft;

        DisplayEmployeeLeft();
    }

    public override void ChangeSizeToRight(IScenario scenario)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = paperRight;

        DisplayEmployeeRight(scenario);
    }

    private void DisplayEmployeeRight(IScenario scenario)
    {
        var tester = scenario.GetTester();

        fullName.text = tester.GetFullName();
        email.text = tester.GetEmail();
        dateStarted.text = tester.GetStartedDate().ToString("MM/dd/yyyy");
        dateOfExpiry.text = tester.GetExpiryDate().ToString("MM/dd/yyyy");

        if (transform.GetChild(0).gameObject.activeInHierarchy == false)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void DisplayEmployeeLeft()
    {
        if (transform.GetChild(0).gameObject.activeInHierarchy == true)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public override void TurnOnInspectorMode()
    {
        gameObject.GetComponent<Image>().raycastTarget = false;

        fullName.raycastTarget = true;
        email.raycastTarget = true;
        dateStarted.raycastTarget = true;
        dateOfExpiry.raycastTarget = true;
}

    public override void TurnOffInspectorMode()
    {
        gameObject.GetComponent<Image>().raycastTarget = true;

        fullName.raycastTarget = false;
        email.raycastTarget = false;
        dateStarted.raycastTarget = false;
        dateOfExpiry.raycastTarget = false;
    }
}
