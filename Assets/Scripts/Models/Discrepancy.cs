using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discrepancy
{
    private string firstTag;
    private string secondTag;
    private Dialogue dialogue;

    public Discrepancy(string tag, string tag2)
    {
        firstTag = tag;
        secondTag = tag2;
    }

    public Discrepancy(string tag, string tag2, Dialogue dialogue)
    {
        firstTag = tag;
        secondTag = tag2;
        this.dialogue = dialogue;
    }

    public string GetFirstTag()
    {
        return firstTag;
    }

    public string GetSecondTag()
    {
        return secondTag;
    }

    public Dialogue GetDialogue()
    {
        if(dialogue == null)
        {
            return null;
        }
        else
        {
            return dialogue;
        }
    }
}
