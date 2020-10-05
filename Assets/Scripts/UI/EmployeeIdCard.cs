using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeIdCard : Card
{
    public override void ChangeSizeToLeft(ReportType type)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = employeePaperLeft;
        changeSize = !changeSize;

        if (transform.GetChild(0).gameObject.activeInHierarchy == true)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public override void ChangeSizeToRight(ReportType type)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = employeePaperRight;
        changeSize = !changeSize;

        if (transform.GetChild(0).gameObject.activeInHierarchy == false)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
