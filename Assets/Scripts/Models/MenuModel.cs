using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuModel : IMenuModel
{
    public DateTime CurrentDay { get; set; }

    public MenuModel()
    {
        //PlayerPrefs.DeleteAll();
    }
}
