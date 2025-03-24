using GameLib.DI;
using QS.Api.Common;
using QS.Chara;
using QS.Common;
using QS.Common.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace QS.Agent
{
    /// <summary>
    /// Agent的职责就是发布指令给所控制的 Character
    /// 
    /// Agent 由几部分组成，感知，寻路，攻击。不同状态下的这些都不一样
    /// 
    /// Free 状态不攻击，只有感知与寻路
    /// 只有Active 状态会进行攻击
    /// 
    /// 感知负责获取攻击目标的位置
    /// 寻路负责根据攻击目标发送移动指定，
    /// 战斗负责选择技能指令进行发送
    /// 
    /// Agent 是主动的状态机，最好拥有自己的生命周期
    /// </summary>
    public class SamuraiAgent : AgentTemplate
    {
        public Transform player;

        IState<AgentState> focusState;
        IState<AgentState> freeState;

        protected override void Start()
        {
            GetComponent<CharaControlTemplate>().meta = new CharaControlMeta
            {
                right = () => Vector3.right,
                forward = () => Vector3.forward,
                up = () => Vector3.up,
                runSpeed = 3f,
                walkSpeed = 1.2f,
                height = 1.7f,
                radius = 0.5f,
                fallGrivity = 9.8f,
            };
            base.Start();


            Steering = new NavSteering(this);
            Sensor = new FocusSensor(new List<Transform>() { player }.ToArray());
            freeState = new SamuraiFree(this);
            focusState = IState<AgentState>.Unit;

            AgentUtil.LoadLuaAgentState(this,
                                        AgentState.Focus,
                                        "Lua_GameAI_Samurai_Focus",
                                        (s) => focusState = s);

        }

        public override IState<AgentState> GetState(AgentState state)
        {
            return state switch
            {
                AgentState.Free => freeState,
                AgentState.Focus => focusState,
                _ => throw new System.NotImplementedException(),
            };
        }

    }
}