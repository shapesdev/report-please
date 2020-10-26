using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldData : MonoBehaviour
{
    private string myData = string.Empty;

    public void SetData(string data)
    {
        myData = data;
    }

    public string GetData()
    {
        return myData;
    }
}
