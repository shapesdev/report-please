using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportCard : Card
{
    [SerializeField]
    private GameObject response;
    [SerializeField]
    private GameObject editorBug;
    [SerializeField]
    private GameObject packageBug;

    public override void ChangeSizeToLeft()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = reportPaperLeft;
        changeSize = !changeSize;

        if (transform.GetChild(0).gameObject.activeInHierarchy == true)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public override void ChangeSizeToRight()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = reportPaperRight;
        changeSize = !changeSize;

        if (transform.GetChild(0).gameObject.activeInHierarchy == false)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
