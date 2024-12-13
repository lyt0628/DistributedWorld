using UnityEngine;
using GameLib.DI;
using QS.Api.Common;
using NLua;
using Tomlet;

public class DepsGlobal : ModuleGlobal<DepsGlobal>
{
    internal IDIContext DI = IDIContext.New();
    public DepsGlobal()
    {
        DI.BindExternalInstance(QS.Api.Deps.DINames.Lua_Skill, new Lua());
    }

    protected override IDIContext DIContext => DI;

    public override void ProvideBinding(IDIContext context)
    {
        context.BindExternalInstance(DI.GetInstance<Lua>());
        
        var ax = new AX()
        {
            name = "666aaa"
        };
       var doc =  TomletMain.DocumentFrom(ax);
        Debug.Log(doc.SerializedValue);
    }
    class AX
    {
        public string name;
    }
}
