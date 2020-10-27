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
    private CloseType correctCloseType;
    private CloseType testerClosedType;
    private Discrepancy discrepancy;

    public Response(int id, DateTime userReply, DateTime testerSent, string email, string emailFrom, Tester tester, Discrepancy discrepancy)
    {
        caseID = id;
        lastUserReply = userReply;
        dateSent = testerSent;
        emailSentFrom = emailFrom;
        this.email = email;
        this.tester = tester;
        this.discrepancy = discrepancy;
    }

    public Response(int id, DateTime userReply, DateTime testerSent, string email, string emailFrom, Tester tester, CloseType testerClose, CloseType correctCloseType, Discrepancy discrepancy)
    {
        caseID = id;
        lastUserReply = userReply;
        dateSent = testerSent;
        emailSentFrom = emailFrom;
        this.email = email;
        this.tester = tester;
        this.correctCloseType = correctCloseType;
        testerClosedType = testerClose;
        this.discrepancy = discrepancy;
    }

    public Discrepancy GetDiscrepancy()
    {
        return discrepancy;
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

    public bool WasItClosedCorrectly()
    {
        return correctCloseType == testerClosedType;
    }

    public CloseType GetCloseType()
    {
        return testerClosedType;
    }

    public string GetFirstAffected()
    {
        return string.Empty;
    }
}
