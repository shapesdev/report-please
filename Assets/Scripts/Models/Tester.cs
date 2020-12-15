using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tester
{
    private int id;
    private string name;
    private string surname;
    private string email;
    private DateTime dateStarted;
    private DateTime dateOfExpiry;
    private string walkInWords;

    public Tester(int id, string walkInWords)
    {
        this.id = id;
        this.walkInWords = walkInWords;
    }

    public Tester(int id, string name, string surname, string email, DateTime dateStart, DateTime expiry, string walkInWords)
    {
        this.id = id;
        this.name = name;
        this.surname = surname;
        this.email = email;
        dateStarted = dateStart;
        dateOfExpiry = expiry;
        this.walkInWords = walkInWords;
    }

    public int GetId()
    {
        return id;
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

    public string GetWalkInWords()
    {
        return walkInWords;
    }
}
