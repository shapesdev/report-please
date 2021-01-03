using System;
using UnityEngine;

public class EndlessGameFactory
{
    public EndlessGameController controller { get; private set; }
    public EndlessGameModel model { get; private set; }
    public EndlessGameView view { get; private set; }

    private GameObject instance;

    public void Load(GameObject gamePrefab, RuleBookSO ruleBook, Sprite[] sprites)
    {
        instance = GameObject.Instantiate(gamePrefab);

        view = instance.GetComponent<EndlessGameView>();
        var selectionView = instance.GetComponentInChildren<GameSelectionView>();
        var stampView = instance.GetComponentInChildren<GameStampView>();
        var lineView = instance.GetComponentInChildren<LineView>();
        var dialogueView = instance.GetComponentInChildren<DialogueView>();
        var characterView = instance.GetComponentInChildren<CharacterView>();

        model = new EndlessGameModel(ruleBook, sprites);
        controller = new EndlessGameController(model, view, selectionView, stampView, lineView, dialogueView, characterView);
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
