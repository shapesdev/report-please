using UnityEngine;

public class App : MonoBehaviour
{
    public static App instance;

    [SerializeField]
    private GameObject storyGamePrefab;
    [SerializeField]
    private GameObject menuPrefab;
    [SerializeField]
    private RuleBookSO ruleBook;

    private StoryGameFactory storyGameFactory;
    private MenuFactory menuFactory;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        instance = this;
        storyGameFactory = new StoryGameFactory();
        menuFactory = new MenuFactory();
        menuFactory.Load(menuPrefab);
    }

    public void Load()
    {
        if(menuFactory.IsLoaded())
        {
            menuFactory.Unload();
            storyGameFactory.Load(storyGamePrefab, ruleBook);
        }
        else if (storyGameFactory.IsLoaded())
        {
            storyGameFactory.Unload();
            menuFactory.Load(menuPrefab);
        }
    }

    public void LoadNextDay()
    {
        if (PlayerPrefs.GetInt("CurrentDay") < 14)
        {
            storyGameFactory.Unload();
            storyGameFactory.Load(storyGamePrefab, ruleBook);
        }
        else
        {
            Load();
        }
    }
}
