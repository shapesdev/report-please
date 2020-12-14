using System;
using UnityEngine;

public class StoryGameFactory
{
    public StoryGameController controller { get; private set; }
    public StoryGameModel model { get; private set; }
    public StoryGameView view { get; private set; }

    private GameObject instance;

    public void Load(GameObject gamePrefab, RuleBookSO ruleBook, Sprite[] sprites)
    {
        instance = GameObject.Instantiate(gamePrefab);

        view = instance.GetComponent<StoryGameView>();
        var selectionView = instance.GetComponentInChildren<GameSelectionView>();
        var stampView = instance.GetComponentInChildren<GameStampView>();
        var lineView = instance.GetComponentInChildren<LineView>();

        model = new StoryGameModel(ruleBook, sprites);
        controller = new StoryGameController(model, view, selectionView, stampView, lineView);
    }

    public void Unload()
    {
        GameObject.Destroy(instance);
    }

    public bool IsLoaded()
    {
        if (instance != null)
        {
            return true;
        }
        return false;
    }
}
