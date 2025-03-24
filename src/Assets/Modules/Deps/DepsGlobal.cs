using GameLib.DI;
using QS.Api.Common;
using QS.Common;
using QS.Deps;
using System.Collections.Generic;
using Tomlet;
using Tomlet.Exceptions;
using Tomlet.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;



/// <summary>
/// 这个模块处理三方依赖的封装和基础预处理。对于依赖的资源统一从这里分配
/// </summary>
public class DepsGlobal : ModuleGlobal<DepsGlobal>
{


    readonly TomlParser tomlParser = new();

    public DepsGlobal()
    {
        LoadOp = new UnitAsyncOp<IModuleGlobal>(this);
    }

    protected override AsyncOpBase<IModuleGlobal> LoadOp { get; }

    

    protected override void DoSetupBinding(IDIContext globalContext, IDIContext moduleContext)
    {

        globalContext
            .BindExternalInstance(tomlParser);

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

}
