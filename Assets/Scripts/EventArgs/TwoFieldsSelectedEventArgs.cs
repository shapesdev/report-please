using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TwoFieldsSelectedEventArgs : EventArgs
{
    public GameObject firstField;
    public GameObject secondField;

    public TwoFieldsSelectedEventArgs(GameObject field1, GameObject field2)
    {
        firstField = field1;
        secondField = field2;
    }
}
