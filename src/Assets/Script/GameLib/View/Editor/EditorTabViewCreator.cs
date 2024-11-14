
using System;
using UnityEditor;
using GameLib.View;

public class EditorTabViewCreator 
{
    [MenuItem("GameObject/GameLib/UI/Tab View")]
    static void CreateViewFacade()
    {
        TabView tabView = new();
        tabView.Show();
    }
}
