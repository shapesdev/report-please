using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MenuController
{
    private readonly IMenuModel model;
    private readonly IMenuView view;

    public static event Action<int> OnMenuInitialized;

    public MenuController(IMenuModel model, IMenuView view)
    {
        this.model = model;
        this.view = view;

        view.OnStoryButtonPressed += View_OnStoryButtonPressed;

        OnMenuInitialized?.Invoke(0);
    }

    private void View_OnStoryButtonPressed(object sender, StoryButtonPressedEventArgs e)
    {
        if(model.CurrentDay.Day == 10)
        {
            Debug.Log("Show something lol");
            App.instance.Load();
        }
        else
        {
            App.instance.Load();
        }
    }
}
