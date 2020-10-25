﻿using System.Collections;
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

    public override void Init(IScenario scenario)
    {
        if(scenario.GetReportType() == ReportType.Response)
        {
            var response = (Response)scenario;

            caseId.text = response.GetCaseID().ToString();
            reply.text = response.GetEmail();
            dateSent.text = response.GetDateSent().ToString("dd/MM/yyyy");
            lastReplyDate.text = response.GetLastReplyDate().ToString("dd/MM/yyyy");
            emailSentFrom.text = response.GetEmailSentFrom();
            if (response.GetCloseType() == CloseType.Empty)
            {
                status.text = "Status: Active (New)";
            }
            else
            {
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
        caseId.raycastTarget = true;
        reply.raycastTarget = true;
        dateSent.raycastTarget = true;
        lastReplyDate.raycastTarget = true;
        emailSentFrom.raycastTarget = true;
        status.raycastTarget = true;
    }

    public override void TurnOffRaycast()
    {
        caseId.raycastTarget = false;
        reply.raycastTarget = false;
        dateSent.raycastTarget = false;
        lastReplyDate.raycastTarget = false;
        emailSentFrom.raycastTarget = false;
        status.raycastTarget = false;
    }
}
