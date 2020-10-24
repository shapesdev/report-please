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

        model = new GameModel(ruleBook);
        view = instance.GetComponent<GameView>();
        controller = new GameController(model, view);
    }

    public void Unload()
    {
        GameObject.Destroy(instance);
    }
}
