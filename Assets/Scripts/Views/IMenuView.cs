using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IMenuView 
{
    event EventHandler<StoryButtonPressedEventArgs> OnStoryButtonPressed;
}
