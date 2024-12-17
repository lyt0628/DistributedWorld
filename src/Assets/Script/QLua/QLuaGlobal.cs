using GameLib.DI;
using NLua;
using QS.Api.Common;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Service;
using QS.Executor;
using QS.GameLib.Pattern.Pipeline;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QS.QLua
{
    public class QLuaGlobal : ModuleGlobal<QLuaGlobal>
    {

        public static void Log(string arg)
        {
            Debug.Log("XLaxaa"+arg);
        }

        internal IDIContext DI = IDIContext.New();
        protected override IDIContext DIContext => DI;

        public QLuaGlobal()
        {
            ExecutorGlobal.Instance.ProvideBinding(DI);
            DI.BindExternalInstance(DepsGlobal.Instance.GetInstance<Lua>(Api.Deps.DINames.Lua_Skill));
            //var lua = DI.GetInstance<Lua>(Api.Deps.DINames.Lua_Skill);
            //var instructionFactory = DI.GetInstance<IInstructionFactory>();
            //var instructionHandlerFactory = DI.GetInstance<IHandlerFactory>();
            //lua["i"] = instructionFactory.Instantiate("BD", null);
            //lua["h"] = instructionHandlerFactory.Instantiate();
            //IPipelineHandler h = (IPipelineHandler)lua["h"];
            //lua["e"] = DI.GetInstance<IExecutor>(Api.Executor.DINames.BaseExecutor);
            //IExecutor e = (IExecutor)lua["e"];
            //e.AddLast("uuid", h);

            //lua.RegisterFunction("DebugLog", null, typeof(QLuaGlobal).GetMethod("Log"));
            //lua.DoFile("Assets/LuaScript/QLua/Test.lua");
        }

        public override void ProvideBinding(IDIContext context)
        {
        }
    }

}