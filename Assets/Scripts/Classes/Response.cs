using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Response : IScenario
{
    private int caseID;
    private DateTime lastReply;
    private DateTime dateSent;
    private string emailSentFrom;
    private string email;

    public Response(int id, DateTime reply, DateTime sent, string emailFrom, string email)
    {
        caseID = id;
        lastReply = reply;
        dateSent = sent;
        emailSentFrom = emailFrom;
        this.email = email;
    }
}
