using UnityEngine;
using System;

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
        view.OnEndlessButtonPressed += View_OnEndlessButtonPressed;

        OnMenuInitialized?.Invoke(0);
    }

    private void View_OnEndlessButtonPressed(object sender, EndlessButtonPressedEventArgs e)
    {
        App.instance.LoadEndlessGame(false);
    }

    private void View_OnStoryButtonPressed(object sender, StoryButtonPressedEventArgs e)
    {
        if (model.CurrentDay.Day == 10)
        {
            App.instance.LoadStoryGame();
        }
        else
        {
            view.ShowContinuePanel(model.CurrentDay);
        }
    }
}
