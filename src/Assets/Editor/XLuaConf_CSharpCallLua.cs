



using QS.PlayerControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using XLua;
using static QS.PlayerControl.PlayerControls;

public static class XLuaConf_CSharpCallLua
{

    [CSharpCallLua]
    public static List<Type> mymodule_cs_call_lua_list = new()
    {
        typeof(Action<UnityEngine.InputSystem.InputAction.CallbackContext>)
        //typeof(Dictionary<string, object>),
        //typeof(GameObject),
        //typeof(Vector2),
        //typeof(Vector3),
        //typeof(VisualElement),
        //typeof(StyleLength),
        //typeof(IStyle)
    };



    //[Hotfix]
    //public static List<Type> by_property
    //{
    //    get
    //    {

    //        return new List<Type>()
    //            .Union((from type in Assembly.Load("QS.Agent").GetTypes()
    //                    where type.Namespace == "QS.Agent"
    //                    select type).ToArray())
    //            .ToList();
    //    }
    //}
}