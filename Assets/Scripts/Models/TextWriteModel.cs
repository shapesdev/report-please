using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriterModel
{
    private Text uiText;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;

    public static event Action OnTextWrite;

    public TextWriterModel(Text uiText, string textToWrite, float timePerCharacter)
    {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
    }

    public void Update()
    {
        if(uiText != null)
        {
            timer -= Time.deltaTime;
            while(timer <= 0f)
            {
                var lastChar = textToWrite[characterIndex];
                if(lastChar == ',')
                {
                    timer += (timePerCharacter * 5);
                }
                else
                {
                    timer += timePerCharacter;
                }

                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);
                uiText.text = text;
                OnTextWrite?.Invoke();

                if(characterIndex >= textToWrite.Length)
                {
                    uiText = null;
                    return;
                }
            }
        }
    }
}
