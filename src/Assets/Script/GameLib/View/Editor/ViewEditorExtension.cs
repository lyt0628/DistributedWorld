using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GameLib.View;

public class View 
{
    [MenuItem("GameObject/GameLib/View/Create ViewNode")]
    static void CreateViewFacade()
    {
        var facade = new GameObject();
        facade.name = "ViewNode";

    }
}
