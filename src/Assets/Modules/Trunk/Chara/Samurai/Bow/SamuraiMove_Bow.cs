

using QS.Chara.Abilities;
using QS.Common;
using QS.Common.FSM;
using System.Linq;
using UnityEngine;

namespace QS.Chara
{
    /// <summary>
    /// 这种情况下的技能的状态控制是耦合的，控制状态和技能是强相关的。除非
    /// 任务上添加获取装备状态的接口，做中介者来解耦
    /// </summary>
    public class SamuraiMove_Bow : CharaStateBase
    {
        readonly Animator animator;
        readonly LoopQueue<Vector3> m_VelocityBuf = new(3, true);
        public SamuraiMove_Bow(CharaControlTemplate controlFSM) : base(controlFSM)
        {
            animator = controlFSM.GetComponent<Animator>();
            m_VelocityBuf.Clear(Vector3.zero);
        }

        public override CharaState State
        {
            get
            {

                //Debug.Log(ControlFSM.HorizontalSpeed);
                return ControlFSM.HorizontalSpeed switch
                {
                    > 3.0f => CharaState.Runing,
                    > 0.1f => CharaState.Walking,
                    >= 0f => CharaState.Idle,
                    _ => throw new System.NotImplementedException(),
                };
            }
        }

        public override ProcessTime ProcessTime => ProcessTime.UpdateAndAnimationMove;

        public override void Process()
        {
            if (ControlFSM.CurrentProcessTime is ProcessTime.Update)
            {
                var localVelocity = ControlFSM.LocalVelocity;
                var lastLocalVelocity = m_VelocityBuf.Aggregate(Vector3.zero, (l, r) => l + r) / m_VelocityBuf.Count;
                var dampVelocity = Vector3.Lerp(lastLocalVelocity, localVelocity, 5 * Time.deltaTime);

                animator.SetFloat("RightSpeed", dampVelocity.x);
                animator.SetFloat("ForwardSpeed", dampVelocity.z);
                animator.SetFloat("MoveScale", localVelocity.magnitude / lastLocalVelocity.magnitude);

                m_VelocityBuf.Push(localVelocity);
            }

            else if (ControlFSM.CurrentProcessTime is ProcessTime.AnimationMove)
            {
                var disp = animator.deltaPosition;
                //Debug.Log(disp);
                ControlFSM.Motor.UnclampedDisplacement = new Vector3(disp.x, ControlFSM.Motor.UnclampedDisplacement.y, disp.z);
                ControlFSM.ApplyDefaultDosplacement();
                ControlFSM.ApplyDefaultRotation();
            }
        }

        public override void Exit()
        {
            m_VelocityBuf.Clear(Vector3.zero);

        }
    }
}