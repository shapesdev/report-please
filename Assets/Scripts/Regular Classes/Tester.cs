using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tester
{
    private string name;
    private string surname;
    private string email;
    private DateTime dateStarted;
    private DateTime dateOfExpiry;

    public Tester(string name, string surname, string email, DateTime dateStart, DateTime expiry)
    {
        this.name = name;
        this.surname = surname;
        this.email = email;
        dateStarted = dateStart;
        dateOfExpiry = expiry;
    }

    public string GetName()
    {
        return name;
    }

    public string GetSurname()
    {
        return surname;
    }

    public string GetFullName()
    {
        return name + " " + surname;
    }

    public string GetEmail()
    {
        return email;
    }

    public DateTime GetStartedDate()
    {
        return dateStarted;
    }

    public DateTime GetExpiryDate()
    {
        return dateOfExpiry;
    }
}
