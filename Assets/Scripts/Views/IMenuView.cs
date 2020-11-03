using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IMenuView 
{
    void ShowContinuePanel(DateTime time);

    event EventHandler<StoryButtonPressedEventArgs> OnStoryButtonPressed;
}
