

using GameLib.DI;
using QS.Common.Util;
using QS.Skill;
using UnityEngine;

namespace QS.Agent
{
    class KatanaLightAttackAction : AIActionBase
    {
        [Injected]
        readonly KatanaLightAttackInstr skInstr;
        readonly AgentTemplate agent;
        readonly Transform target;
        public KatanaLightAttackAction(AgentTemplate agent, Transform target)
        {
            this.agent = agent;
            this.target = target;
            skInstr = AgentGlobal.Instance.GetInstance<KatanaLightAttackInstr>();
        }

        public override float Span => .7f;

        public override bool IsValid
        {
            get
            {
                return TransformUtil.IsInSectorArea(agent.transform, target, Common.RelativeDir.Forward, 180, 3f);
            }
        }

        public override int Priority => 5;

        public override void Enter()
        {
            base.Enter();
            agent.Chara.Execute(skInstr);

        }
    }
}