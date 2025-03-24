

using GameLib.DI;
using QS.Chara;
using UnityEngine;

namespace QS.Agent
{
    class DodgeAction : AIActionBase
    {
        [Injected]
        readonly IMoveInstr moveInstr;
        [Injected]
        readonly IDodgeInstr dodgeInstr;

        readonly AgentTemplate agent;
        public DodgeAction(AgentTemplate agent, Vector2 dir, float span, int priority)
        {
            this.agent = agent;
            Span = span;
            Priority = priority;
            AgentGlobal.Instance.Inject(this);
            moveInstr.value = dir;
        }

        public override float Span { get; }

        public override int Priority { get; }

        public override void Enter()
        {
            base.Enter();
            agent.Chara.Execute(moveInstr);
            agent.Chara.Execute(dodgeInstr);
        }

    }

}