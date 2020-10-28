using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class App : MonoBehaviour
{
    [SerializeField]
    private GameObject gamePrefab;
    [SerializeField]
    private GameObject menuPrefab;
    [SerializeField]
    private RuleBookSO ruleBook;

    private GameFactory gameFactory;


    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        gameFactory = new GameFactory();
        gameFactory.Load(gamePrefab, ruleBook);
    }
}
