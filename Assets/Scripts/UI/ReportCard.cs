using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ReportType
{
    Response, EditorBug, PackageBug
}

public class ReportCard : Card
{
    [SerializeField]
    private GameObject response;
    [SerializeField]
    private GameObject editorBug;
    [SerializeField]
    private GameObject packageBug;

    public override void ChangeSizeToLeft(ReportType reportType)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = reportPaperLeft;
        changeSize = !changeSize;

        if(reportType == ReportType.Response)
        {
            response.SetActive(false);
        }
        else if(reportType == ReportType.EditorBug)
        {
            editorBug.SetActive(false);
        }
        else if(reportType == ReportType.PackageBug)
        {
            packageBug.SetActive(false);
        }
    }

    public override void ChangeSizeToRight(ReportType reportType)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = reportPaperRight;
        changeSize = !changeSize;

        if (reportType == ReportType.Response)
        {
            response.SetActive(true);
        }
        else if (reportType == ReportType.EditorBug)
        {
            editorBug.SetActive(true);
        }
        else if (reportType == ReportType.PackageBug)
        {
            packageBug.SetActive(true);
        }
    }
}
