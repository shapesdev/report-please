using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    private string inspectorWords;
    private string testerWords;

    public Dialogue(string inspector, string tester)
    {
        inspectorWords = inspector;
        testerWords = tester;
    }

    public string GetInspectorWords()
    {
        return inspectorWords;
    }

    public string GetTesterWords()
    {
        return testerWords;
    }
}
