

using Cysharp.Threading.Tasks;
using QS.Api.Common;
using QS.Common;
using QS.Common.FSM;
using QS.Trunk;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace QS.Agent
{
    class LuaAgentStateLoadOp : AsyncOpBase<IState<AgentState>>
    {
        readonly AgentTemplate agent;
        readonly AgentState state;
        readonly string address;

        public LuaAgentStateLoadOp(AgentTemplate agent, AgentState state, string address)
        {
            this.agent = agent != null ? agent : throw new ArgumentNullException(nameof(agent));
            this.state = state;
            this.address = address ?? throw new ArgumentNullException(nameof(address));
        }

        protected override async void Execute()
        {
            var h_LoadLuaModule = TrunkGlobal.Instance.Context.GetInstance<IAsyncOpHandle>(Trunk.DINames.ASYNCOP_HANDLE_LOAD_LUA_MODULES);
            var h_Script = Addressables.LoadAssetAsync<TextAsset>(address);
            await UniTask.WhenAll(h_LoadLuaModule.Task.AsUniTask(), 
                                  h_LoadLuaModule.Task.AsUniTask());

            Complete(new LuaAgentState(agent, state, h_Script.Result.text));
        }
    }
}