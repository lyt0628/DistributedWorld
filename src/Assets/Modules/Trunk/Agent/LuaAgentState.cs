

using GameLib.DI;
using System;
using XLua;

namespace QS.Agent
{
    /// <summary>
    ///  负责创建沙盒环境供Lua运行
    /// </summary>

    class LuaAgentState : AgentStateBase
    {
        [Injected]
        readonly LuaEnv luaEnv;
        readonly LuaTable scopedEnv;

        readonly Action luaEnter;
        readonly Action luaExit;
        readonly Action luaProcess;

        public LuaAgentState(AgentTemplate agent, AgentState state, string script)
            : base(agent)
        {

            State = state;
            AgentGlobal.Instance.Inject(this);
            scopedEnv = luaEnv.NewTable();


            using (LuaTable meta = luaEnv.NewTable())
            {
                meta.Set("__index", luaEnv.Global);
                scopedEnv.SetMetaTable(meta);
            }
            scopedEnv.Set("self", agent);

            luaEnv.DoString(script, $"{agent.name}[{State}]", scopedEnv);

            scopedEnv.Get("enter", out luaEnter);
            scopedEnv.Get("process", out luaProcess);
            scopedEnv.Get("exit", out luaExit);
        }


        public override AgentState State { get; }

        public override void Enter() => luaEnter?.Invoke();

        public override void Process() => luaProcess?.Invoke();

        public override void Exit() => luaExit?.Invoke();
    }
}