using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResponseDisplayer : GeneralDisplayer
{
    [SerializeField]
    private TMP_Text caseId;
    [SerializeField]
    private TMP_Text reply;
    [SerializeField]
    private TMP_Text dateSent;
    [SerializeField]
    private TMP_Text lastReplyDate;
    [SerializeField]
    private TMP_Text emailSentFrom;
    [SerializeField]
    private TMP_Text status;
    [SerializeField]
    private TMP_Text title;

    [SerializeField]
    private TMP_Text[] allTexts;

    public override void Init(IScenario scenario)
    {
        if(scenario.GetReportType() == ReportType.Response)
        {
            var response = (Response)scenario;

            title.text = response.GetTitle();
            caseId.text = "Case id: " + response.GetCaseID().ToString();
            reply.text = response.GetEmail();
            dateSent.text = response.GetDateSent().ToString("yyyy/MM/dd");
            lastReplyDate.text = response.GetLastReplyDate().ToString("yyyy/MM/dd");
            emailSentFrom.text = response.GetEmailSentFrom();
            if (response.GetCloseType() == CloseType.Empty)
            {
                status.text = "";
            }
            else
            {
                if(response.GetCloseType() == CloseType.PendingInformation)
                {
                    status.text = "Status: Pending Information";
                }
                if(response.GetCloseType() == CloseType.Active)
                {
                    status.text = "Status: Active (New)";
                }

                if (response.GetCloseType() == CloseType.NotQualified)
                {
                    status.text = "Status: Closed (Not Qualified)";
                }
                else
                {
                    status.text = "Status: Closed (" + response.GetCloseType() + ")";
                }
            }
        }
    }

    public override void RightDisplay(ReportType type)
    {
        if(type == ReportType.Response) { gameObject.SetActive(true); }
    }

    public override void LeftDisplay(ReportType type)
    {
        if(type == ReportType.Response) { gameObject.SetActive(false); }
    }

    public override void TurnOnRaycast()
    {
        foreach(var txt in allTexts)
        {
            txt.raycastTarget = true;
            txt.color = ColorHelper.instance.InspectorModeColor;
        }
    }

    public override void TurnOffRaycast()
    {
        foreach (var txt in allTexts)
        {
            txt.raycastTarget = false;
            txt.color = ColorHelper.instance.NormalModeColor;
        }
    }
}
