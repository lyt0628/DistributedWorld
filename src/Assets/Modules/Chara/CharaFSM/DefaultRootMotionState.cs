



using QS.Chara.Abilities;
using QS.Common.FSM;
using UnityEngine;

namespace QS.Chara
{
    public class DefaultRootMotionState : CharaStateBase
    {
        readonly Animator animator;
        public DefaultRootMotionState(CharaControlTemplate controlFSM) : base(controlFSM)
        {
            animator = controlFSM.GetComponent<Animator>();
        }

        public override CharaState State => CharaState.RootMotion;
        public override ProcessTime ProcessTime => ProcessTime.AnimationMove;

        public override void Process()
        {
            ControlFSM.Motor.UnclampedDisplacement = animator.deltaPosition;
            ControlFSM.ApplyDefaultDosplacement();
            //ControlFSM.ApplyDefaultRotation();
        }
    }
}