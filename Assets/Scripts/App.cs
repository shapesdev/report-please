using UnityEngine;

public class App : MonoBehaviour
{
    public static App instance;

    float timer = 1.0f;
    float waitTime = 0f;
    bool connected = false;

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
    [SerializeField]
    private GameObject connectionPanel;

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

    public void QuitGame()
    {
        Application.Quit();
    }
}
