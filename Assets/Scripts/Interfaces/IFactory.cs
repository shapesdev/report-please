using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory 
{
    /*    void Initialize();
        void ManagerUpdate();
        void FixedManagerUpdate();*/

    void Load(GameObject go);
    void Unload();
}
