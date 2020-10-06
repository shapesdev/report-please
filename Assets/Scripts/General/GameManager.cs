using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public struct daysStruct
    {
        public Scenario[] scenarios;
    }

    private List<IManager> IManagers;

    public daysStruct[] days;

    private void Awake()
    {
        IManagers = new List<IManager>();

        var managers = FindObjectsOfType<MonoBehaviour>().OfType<IManager>();

        foreach(var a in managers)
        {
            IManagers.Add(a);
            a.Initialize();
        }
    }

    private void Update()
    {
        foreach (var manager in IManagers)
        {
            manager.ManagerUpdate();
        }
    }

    private void FixedUpdate()
    {
        foreach (var manager in IManagers)
        {
            manager.FixedManagerUpdate();
        }
    }
}
