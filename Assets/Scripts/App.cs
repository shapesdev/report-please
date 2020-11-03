using UnityEngine;

public class App : MonoBehaviour
{
    public static App instance;

    [SerializeField]
    private GameObject gamePrefab;
    [SerializeField]
    private GameObject menuPrefab;
    [SerializeField]
    private RuleBookSO ruleBook;

    private GameFactory gameFactory;
    private MenuFactory menuFactory;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        instance = this;
        gameFactory = new GameFactory();
        menuFactory = new MenuFactory();
        menuFactory.Load(menuPrefab);
    }

    public void Load()
    {
        if(menuFactory.IsLoaded())
        {
            menuFactory.Unload();
            gameFactory.Load(gamePrefab, ruleBook);
        }
        else if (gameFactory.IsLoaded())
        {
            gameFactory.Unload();
            menuFactory.Load(menuPrefab);
        }
    }

    public void LoadNextDay()
    {
        if (PlayerPrefs.GetInt("CurrentDay") < 14)
        {
            gameFactory.Unload();
            gameFactory.Load(gamePrefab, ruleBook);
        }
        else
        {
            Load();
        }
    }
}
