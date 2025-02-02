﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AreasNGrabbags
{
    public string area;
    public string grabbag;
}

[System.Serializable]
public struct ReportFieldInfo
{
    public string field;
    public string description;
    public Info[] info;
}

[CreateAssetMenu(fileName = "New Rule Book", menuName = "Rule Book")]
public class RuleBookSO : ScriptableObject
{
    public List<Rules> basicRules;
    public List<AreasNGrabbags> areas;
    public List<ReportFieldInfo> reportFields;
    public List<Versions> versionInfo;
    public List<PackageInfo> packagesInfo;
}

[System.Serializable]
public class Info
{
    public int level;
    public string name;
}

[System.Serializable]
public class Versions
{
    public string stream;
    public string[] versions;
}

[System.Serializable]
public class PackageInfo
{
    public string packageName;
    public string[] versions;
}

[System.Serializable]
public class Rules
{
    public int day;
    public List<string> rules;
}
