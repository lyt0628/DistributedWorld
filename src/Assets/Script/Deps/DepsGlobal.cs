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
            // ����ͨ��ProvideBinding�ṩ����Ϊ���ڵ����Lua�����ǲ���ȫ�ģ�������
            // ��˲��ܱ�¶��ȥ�����ԣ��Ӹ����ϣ��������Lua�������Ӧ�÷���Trunk��Ŷ�
            // �����ô���Ļ��������������������ļ���û��������
            // ��û�취�ģ����ϲ�����ע��ɣ����ǿ��Խ��ܵ�
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
