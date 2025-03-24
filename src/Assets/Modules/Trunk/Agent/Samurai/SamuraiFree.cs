using UnityEngine;

namespace QS.Agent
{
    class SamuraiFree : AgentStateBase
    {
        public override AgentState State => AgentState.Free;

        public SamuraiFree(SamuraiAgent agent) : base(agent)
        {
            this.agent = agent;
        }

        public override void Process()
        {
            var player = agent.Sensor.Enemy;
            if (player != null)
            {
                if (Vector3.Distance(agent.transform.position, player.position) < 10f)
                {
                    agent.SwitchTo(AgentState.Focus);
                }
            }

        }
    }
}