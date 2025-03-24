using UnityEngine;

namespace QS.Agent
{
    /// <summary>
    /// 接近玩家的行动
    /// </summary>
    class ApprochAction : AIActionBase
    {
        readonly AgentTemplate agent;

        public ApprochAction(AgentTemplate agent, Transform target)
        {
            this.agent = agent;
            this.target = target;
            Span = 0f;
        }

        public override bool IsValid
        {
            get
            {
                return Time.time - CreatedTime < 0.3f;
            }
        }

        public override float Span { get; }
        public override int Priority => 10;

        readonly Transform target;

        public override void Process()
        {
            if (target != null)
            {
                agent.Steering.Destination = target.position;
            }
            else
            {
                agent.Steering.Destination = Vector3.zero;
            }
            var moveInstr = agent.Steering.GetTranslateInstr();
            agent.Chara.Execute(moveInstr);
        }
    }

}