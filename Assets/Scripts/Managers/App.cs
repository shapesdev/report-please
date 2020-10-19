using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class App : MonoBehaviour
{
    private List<IManager> managers;

    private void Awake()
    {
        this.managers = new List<IManager>();

        var managers = GetComponentsInChildren<MonoBehaviour>().OfType<IManager>();

        foreach(var a in managers)
        {
            this.managers.Add(a);
            a.Initialize();
        }
    }

    private void Update()
    {
        if(managers.Count > 0)
        {
            foreach (var manager in managers)
            {
                manager.ManagerUpdate();
            }
        }
    }

    private void FixedUpdate()
    {
        if(managers.Count > 0)
        {
            foreach (var manager in managers)
            {
                manager.FixedManagerUpdate();
            }
        }
    }
}
