using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuModel : IMenuModel
{
    public DateTime CurrentDay { get; set; }

    public MenuModel()
    {
        if(PlayerPrefs.GetInt("CurrentDay") == 0)
        {
            CurrentDay = new DateTime(2020, 11, 10);
            PlayerPrefs.SetInt("CurrentDay", CurrentDay.Day);
        }
        else
        {
            CurrentDay = new DateTime(2020, 11, PlayerPrefs.GetInt("CurrentDay"));
        }
    }
}
