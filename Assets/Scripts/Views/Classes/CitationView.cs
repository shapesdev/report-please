using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CitationView : GameGeneralView
{
    public Text citationText;

    public override void ChangeSizeToLeft()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = paperLeft;
        gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 35);

        if (citationText.gameObject.activeInHierarchy == true)
        {
            citationText.gameObject.SetActive(false);
        }
    }

    public override void ChangeSizeToRight()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = paperRight;
        gameObject.GetComponent<RectTransform>().rotation = Quaternion.identity;

        if(citationText.gameObject.activeInHierarchy == false)
        {
            citationText.gameObject.SetActive(true);
        }
    }

    public override void TurnOnInspectorMode()
    {
        gameObject.GetComponent<Image>().raycastTarget = false;

        gameObject.GetComponent<Image>().color = ColorHelper.instance.InspectorModeColor;
        citationText.color = ColorHelper.instance.InspectorModeColor;
    }

    public override void TurnOffInspectorMode()
    {
        gameObject.GetComponent<Image>().raycastTarget = true;

        gameObject.GetComponent<Image>().color = ColorHelper.instance.NormalModeColor;
        citationText.color = ColorHelper.instance.NormalModeColor;
    }
}
