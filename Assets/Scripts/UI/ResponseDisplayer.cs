using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResponseDisplayer : MonoBehaviour
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

    public void LeftDisplay()
    {
        gameObject.SetActive(false);
    }

    public void RightDisplay(IScenario scenario)
    {
        var response = (Response)scenario;

        reply.enableAutoSizing = false;

        caseId.text = response.GetCaseID().ToString();
        reply.text = response.GetEmail();
        dateSent.text = response.GetDateSent().ToString("dd/MM/yyyy");
        lastReplyDate.text = response.GetLastReplyDate().ToString("dd/MM/yyyy");
        emailSentFrom.text = response.GetEmailSentFrom();
        if(response.GetCloseType() == CloseType.Empty)
        {
            status.text = "Status: Active (New)";
        }
        else
        {
            if(response.GetCloseType() == CloseType.NotQualified)
            {
                status.text = "Status: Closed (Not Qualified)";
            }
            else
            {
                status.text = "Status: Closed (" + response.GetCloseType() + ")";
            }
        }

        reply.enableAutoSizing = true;

        gameObject.SetActive(true);
    }
}
