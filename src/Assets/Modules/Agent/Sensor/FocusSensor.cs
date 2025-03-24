

using UnityEngine;

namespace QS.Agent
{
    /// <summary>
    /// 直接锁定了目标的感知器，绝对不会跟丢目标
    /// </summary>
    public class FocusSensor : ISensor
    {
        protected AgentTemplate agent;

        public FocusSensor(AgentTemplate agent)
        {
            this.agent = agent;
        }

        public FocusSensor(Transform[] targets)
        {
            Enemies = targets;
        }

        public Transform[] Enemies { get; }

        public bool EnemyFound => Enemies.Length > 0;

        public Transform Enemy => Enemies[0];



    }
}