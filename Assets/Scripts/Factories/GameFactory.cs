using System;
using UnityEngine;

public class GameFactory
{
    public GameController controller { get; private set; }
    public GameModel model { get; private set; }
    public GameView view { get; private set; }

    private GameObject instance;

    public void Load(GameObject gamePrefab, RuleBookSO ruleBook)
    {
        instance = GameObject.Instantiate(gamePrefab);

        view = instance.GetComponent<GameView>();
        var selectionView = instance.GetComponentInChildren<GameSelectionView>();

        model = new GameModel(ruleBook);
        controller = new GameController(model, view, selectionView);
    }

    public void Unload()
    {
        GameObject.Destroy(instance);
    }
}
