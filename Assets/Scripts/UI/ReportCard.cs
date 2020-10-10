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

    public override void ChangeSizeToLeft(ReportType reportType)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = reportPaperLeft;
        changeSize = !changeSize;

        if(reportType == ReportType.Response)
        {
            ShowLeftResponse();
        }
        else if(reportType == ReportType.EditorBug)
        {
            ShowLeftEditorBug();
        }
        else if(reportType == ReportType.PackageBug)
        {
            ShowLeftPackageBug();
        }
    }

    public override void ChangeSizeToRight(ReportType reportType)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = reportPaperRight;
        changeSize = !changeSize;

        if (reportType == ReportType.Response)
        {
            ShowRightResponse();
        }
        else if (reportType == ReportType.EditorBug)
        {
            ShowRightEditorBug();
        }
        else if (reportType == ReportType.PackageBug)
        {
            ShowRightPackageBug();
        }
    }

    private void ShowRightResponse()
    {
        response.SetActive(true);
    }

    private void ShowRightEditorBug()
    {
        editorBug.SetActive(true);
    }

    private void ShowRightPackageBug()
    {
        packageBug.SetActive(true);
    }

    private void ShowLeftResponse()
    {
        response.SetActive(false);
    }

    private void ShowLeftEditorBug()
    {
        editorBug.SetActive(false);
    }

    private void ShowLeftPackageBug()
    {
        packageBug.SetActive(false);
    }
}
