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
        if (PlayerPrefs.GetInt("CurrentDay") == 0)
        {
            model.CurrentDay = new DateTime(2020, 11, 10);
            PlayerPrefs.SetInt("CurrentDay", model.CurrentDay.Day);
            App.instance.Load();
        }
        else
        {
            model.CurrentDay = new DateTime(2020, 11, PlayerPrefs.GetInt("CurrentDay"));
            view.ShowContinuePanel(model.CurrentDay);
        }
    }
}
