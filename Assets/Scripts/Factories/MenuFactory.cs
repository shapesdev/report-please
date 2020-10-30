using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFactory
{
    public MenuController controller { get; private set; }
    public MenuModel model { get; private set; }
    public MenuView view { get; private set; }

    private GameObject instance;

    public void Load(GameObject menuPrefab)
    {
        instance = GameObject.Instantiate(menuPrefab);

        view = instance.GetComponent<MenuView>();
        model = new MenuModel();

        controller = new MenuController(model, view);
    }

    public void Unload()
    {
        GameObject.Destroy(instance);
    }

    public bool IsLoaded()
    {
        if(instance != null)
        {
            return true;
        }
        return false;
    }
}
