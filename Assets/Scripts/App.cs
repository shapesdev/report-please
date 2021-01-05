using UnityEngine;

public class App : MonoBehaviour
{
    public static App instance;

    public GameType gameType;

    float timer = 1.0f;
    float waitTime = 0f;
    bool connected = false;

    [SerializeField]
    private GameObject storyGamePrefab;
    [SerializeField]
    private GameObject menuPrefab;
    [SerializeField]
    private GameObject endlessGamePrefab;
    [SerializeField]
    private RuleBookSO ruleBook;
    [SerializeField]
    private Sprite[] allCharacterSprites;
    [SerializeField]
    private Sprite[] storyCharacterSprites;
    [SerializeField]
    private GameObject connectionPanel;

    private StoryGameFactory storyGameFactory;
    private MenuFactory menuFactory;
    private EndlessGameFactory endlessGameFactory;

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
        endlessGameFactory = new EndlessGameFactory();
        menuFactory.Load(menuPrefab);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime && connected == false)
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                Debug.Log("IS INTERNET");
                connected = true;
                connectionPanel.SetActive(false);
            }
            timer = 0f;
        }
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            connected = false;
            connectionPanel.SetActive(true);
            Debug.Log("NO INTERNET");
        }
    }

    public void LoadStoryGame()
    {
        if(menuFactory.IsLoaded())
        {
            menuFactory.Unload();
            storyGameFactory.Load(storyGamePrefab, ruleBook, storyCharacterSprites);
            gameType = GameType.Story;
        }
        else if (storyGameFactory.IsLoaded())
        {
            storyGameFactory.Unload();
            menuFactory.Load(menuPrefab);
        }
    }

    public void LoadEndlessGame(bool playAgain)
    {
        if(playAgain)
        {
            endlessGameFactory.Unload();
            endlessGameFactory.Load(endlessGamePrefab, ruleBook, allCharacterSprites);
            gameType = GameType.Endless;
        }
        else
        {
            if (menuFactory.IsLoaded())
            {
                menuFactory.Unload();
                endlessGameFactory.Load(endlessGamePrefab, ruleBook, allCharacterSprites);
                gameType = GameType.Endless;
            }
            else if (endlessGameFactory.IsLoaded())
            {
                endlessGameFactory.Unload();
                menuFactory.Load(menuPrefab);
            }
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
            LoadStoryGame();
        }
    }
}
