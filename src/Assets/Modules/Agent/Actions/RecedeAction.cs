
using UnityEngine;

namespace QS.Agent
{
    /// <summary>
    /// 远离玩家的行动
    /// </summary>
    class RecedeAction : AIActionBase
    {
        readonly AgentTemplate agent;

        public RecedeAction(AgentTemplate agent, Transform target)
        {
            this.agent = agent;
            this.target = target;
        }

        public override float Span { get; } = 0.5f;
        public override int Priority => 10;

        readonly Transform target;

        public override void Process()
        {
            if (target != null)
            {
                var dir = agent.transform.position - target.position;
                dir = dir.normalized;
                agent.Steering.Destination = agent.transform.position + dir;
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