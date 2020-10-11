using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class App : MonoBehaviour
{
    private List<IManager> IManagers;

    private void Awake()
    {
        IManagers = new List<IManager>();

        var managers = GetComponentsInChildren<MonoBehaviour>().OfType<IManager>();

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
