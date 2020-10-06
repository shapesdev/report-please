using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scenario", menuName = "Scenario")]
public class Scenario : ScriptableObject
{
    //[Header("Show Value?")]

    // An example with an enum
    public ShowValueEnum EnumTest = ShowValueEnum.None;
    [DrawIf("EnumTest", ShowValueEnum.ShowValue1)]  //Show if enum is equal to ShowValue1
    public int Value1 = 100;
    [DrawIf("EnumTest", ShowValueEnum.ShowValue2)]  //Show if enum is equal to ShowValue2
    public int Value2 = 200;

    // An example for a bool
    public bool BoolTest = false;
    [DrawIf("BoolTest", true)] // Show if booltest bool is true
    public Vector3 Value;
}


public enum ShowValueEnum
{
    ShowValue1,
    ShowValue2,
    None
}
