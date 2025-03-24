

using GameLib.DI;
using QS.Api.Executor.Domain;
using QS.Chara;
using QS.Stereotype;
using UnityEngine;

namespace QS.Agent
{
    /// <summary>
    /// ������ Unity ����ϵͳ�� �ƶ����
    /// </summary>
    public class NavSteering : ISteering
    {
        readonly AgentTemplate agent;
        [Nullable]
        public Vector3 Destination { get; set; }
        public bool Run { get { return moveInstr.run; } set { moveInstr.run = value; } }

        [Injected]
        readonly IMoveInstr moveInstr;
        public NavSteering(AgentTemplate agent)
        {
            AgentGlobal.Instance.Inject(this);

            this.agent = agent;
        }

        IInstruction ISteering.GetTranslateInstr()
        {
            if (Destination != Vector3.zero)
            {
                var dir = Destination - agent.transform.position;
                dir = dir.normalized;

                moveInstr.value = new Vector2(dir.x, dir.z);
            }
            else
            {
                moveInstr.value = Vector2.zero;
            }
            return moveInstr;
        }
    }
}