using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;




namespace GameLib.Editor
{
    internal static class GameLibEditorConstant
    {
        public static string GAMELIB_HIDEN_DIR = Path.Combine(Application.dataPath, "GameLib~");
        public static string GAMELIB_CACHE_DIR = Path.Combine(GAMELIB_HIDEN_DIR, "Cache");

    }
}