

using UnityEngine;

namespace QS.Agent
{
    /// <summary>
    /// ֱ��������Ŀ��ĸ�֪�������Բ������Ŀ��
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