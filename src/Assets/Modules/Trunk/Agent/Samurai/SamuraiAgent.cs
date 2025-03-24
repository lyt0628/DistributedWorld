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
    /// Agent��ְ����Ƿ���ָ��������Ƶ� Character
    /// 
    /// Agent �ɼ�������ɣ���֪��Ѱ·����������ͬ״̬�µ���Щ����һ��
    /// 
    /// Free ״̬��������ֻ�и�֪��Ѱ·
    /// ֻ��Active ״̬����й���
    /// 
    /// ��֪�����ȡ����Ŀ���λ��
    /// Ѱ·������ݹ���Ŀ�귢���ƶ�ָ����
    /// ս������ѡ����ָ����з���
    /// 
    /// Agent ��������״̬�������ӵ���Լ�����������
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