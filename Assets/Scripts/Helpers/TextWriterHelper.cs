using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriterHelper : MonoBehaviour
{
    public static TextWriterHelper instance;
    private TextWriterModel model;

    private void Start()
    {
        instance = this;
    }

    public void AddWriter(Text uiText, string textToWrite, float timePerCharacter)
    {
        model = new TextWriterModel(uiText, textToWrite, timePerCharacter);
    }

    private void Update()
    {
        if(model != null)
        {
            model.Update();
        }
    }
}
