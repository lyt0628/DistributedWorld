using UnityEngine;
using GameLib.DI;
using QS.Api.Common;
using NLua;
using Tomlet;
using Tomlet.Models;
using Tomlet.Exceptions;
using System;

[CLSCompliant(false)]
public class DepsGlobal : ModuleGlobal<DepsGlobal>
{
    internal IDIContext DI = IDIContext.New();
    public DepsGlobal()
    {
        //var skLua =  new Lua();
        //RegisterDebugFunctions(skLua);
        DI
            .BindExternalInstance(new TomlParser());
            //.BindExternalInstance(QS.Api.Deps.DINames.Lua_GameLogic, new Lua())
            // 不能通过ProvideBinding提供，因为现在的这个Lua环境是不完全的，不可用
            // 因此不能暴露出去，不对，从根本上，这个加载Lua的任务就应该放在Trunk层才对
            // 如果这么做的话，各个层甚至连配置文件都没法加载了
            // 这没办法的，从上层向下注入吧，这是可以接受的
            //.BindExternalInstance(QS.Api.Deps.DINames.Lua_Skill, skLua);

        RegisterTomlType();
    }

    static void RegisterTomlType()
    {
        TomletMain.RegisterMapper<Vector3>(
            vec =>
            {
                var table = new TomlTable();
                table.PutValue("x", new TomlDouble(vec.x));
                table.PutValue("y", new TomlDouble(vec.y));
                table.PutValue("z", new TomlDouble(vec.z));
                return table;
            },
            tomlValue =>
            {
                if (tomlValue is not TomlTable table)
                {
                    throw new TomlTypeMismatchException(typeof(TomlTable), tomlValue.GetType(), typeof(Vector3));
                }
                return new Vector3(table.GetFloat("x"), table.GetFloat("y"), table.GetFloat("z"));
            });

        TomletMain.RegisterMapper<Quaternion>(
            q =>
            {
                return TomletMain.ValueFrom(q.eulerAngles);
            },
            tomlValue =>
            {
                if (tomlValue is not TomlTable table)
                {
                    throw new TomlTypeMismatchException(typeof(TomlTable), tomlValue.GetType(), typeof(Quaternion));
                }
                var q = new Quaternion
                {
                    eulerAngles = TomletMain.To<Vector3>(tomlValue)
                };
                return q;
            });
    }

    protected override IDIContext DIContext => DI;

    public override void ProvideBinding(IDIContext context)
    {
        //context.BindExternalInstance(DI.GetInstance<Lua>());

        // var ax = new AX()
        // {
        //     name = "666aaa"
        // };
        //var doc =  TomletMain.DocumentFrom(ax);
        // Debug.Log(doc.SerializedValue);


        // Debug.Log(Application.dataPath);


    }
    //class AX
    //{
    //    public string name;
    //}

    public static void RegisterDebugFunctions(Lua lua)
    {
        lua["DebugLog"] = new Action<object>(Debug.Log);
        lua["DebugLogWarning"] = new Action<object>(Debug.LogWarning);
        lua["DebugLogError"] = new Action<object>(Debug.LogError);
    }
}
