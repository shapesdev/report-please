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
    [SerializeField]
    private Sprite[] allCharacterSprites;
    [SerializeField]
    private Sprite[] storyCharacterSprites;

    private StoryGameFactory storyGameFactory;
    private MenuFactory menuFactory;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        Init();
    }

    private void Init()
    {
        instance = this;
        storyGameFactory = new StoryGameFactory();
        menuFactory = new MenuFactory();
        menuFactory.Load(menuPrefab);
    }

    private void Export()
    {
        DataExportHelper dataExportHelper = new DataExportHelper();

        dataExportHelper.AppendToReport(
    new string[4]
    {
                Random.Range(0, 11).ToString(),
                Random.Range(0, 11).ToString(),
                Random.Range(0, 11).ToString(),
                Random.Range(0, 11).ToString()
    });
    }

    public void Load()
    {
        if(menuFactory.IsLoaded())
        {
            menuFactory.Unload();
            storyGameFactory.Load(storyGamePrefab, ruleBook, storyCharacterSprites);
        }
        else if (storyGameFactory.IsLoaded())
        {
            storyGameFactory.Unload();
            menuFactory.Load(menuPrefab);
        }
    }

    public void LoadNextDay()
    {
        if (PlayerPrefs.GetInt("CurrentDay") < 15)
        {
            storyGameFactory.Unload();
            storyGameFactory.Load(storyGamePrefab, ruleBook, storyCharacterSprites);
        }
        else
        {
            Load();
        }
    }
}
