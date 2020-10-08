using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Response : IScenario
{
    private int caseID;
    private DateTime lastUserReply;
    private DateTime dateSent;
    private string emailSentFrom;
    private string email;
    private Tester tester;

    public Response(int id, DateTime userReply, DateTime testerSent, string email, string emailFrom, Tester tester)
    {
        caseID = id;
        lastUserReply = userReply;
        dateSent = testerSent;
        emailSentFrom = emailFrom;
        this.email = email;
        this.tester = tester;
    }

    public ReportType GetReportType()
    {
        return ReportType.Response;
    }

    public int GetCaseID()
    {
        return caseID;
    }

    public DateTime GetLastReplyDate()
    {
        return lastUserReply;
    }

    public DateTime GetDateSent()
    {
        return dateSent;
    }

    public string GetEmail()
    {
        return email;
    }

    public string GetEmailSentFrom()
    {
        return emailSentFrom;
    }

    public Tester GetTester()
    {
        return tester;
    }
}
