

using QS.Chara.Abilities;
using QS.Common;
using QS.Common.FSM;
using System.Linq;
using UnityEngine;

namespace QS.Chara
{

    public class SamuraiGrounded_Katana : CharaStateBase
    {
        readonly Animator animator;
        readonly LoopQueue<Vector3> m_VelocityBuf = new(3, true);
        public SamuraiGrounded_Katana(CharaControlTemplate controlFSM)
            : base(controlFSM)
        {
            animator = controlFSM.GetComponent<Animator>();
            Transitions = new ITransition<CharaState>[]
            {
                new SamuraiRunStart_Katana(controlFSM, this),
                new SamuraiRunEnd_Katana(controlFSM, this)
            };
            m_VelocityBuf.Clear(Vector3.zero);
        }




        //public Vector3 dampVelocity;
        public override ITransition<CharaState>[] Transitions { get; }

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
                var lastLocalVelocity = m_VelocityBuf.Aggregate(Vector3.zero, (l, r)=> l + r) / m_VelocityBuf.Count;
                var dampVelocity = Vector3.Lerp(lastLocalVelocity, localVelocity,5 * Time.deltaTime);

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