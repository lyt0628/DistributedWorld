
using QS.Chara.Abilities;
using QS.Common.FSM;
using System;
using UnityEngine;

namespace QS.Chara
{
    class SamuraiRunEnd_Katana : ICharaStateTransition
    {

        public Func<bool> Condition => () =>
        {
            return  controlFSM.GetState(controlFSM.CurrentState).State is CharaState.Runing
                   && AnimUtil.CountMainMoveDir(controlFSM.LocalHorVelocity.normalized) == AnimUtil.RELATIVE_DIR_F
                   && Mathf.Approximately(controlFSM.MoveDir.magnitude, 0);
        };

        public CharaState Target => CharaState.Idle;

        public ProcessTime ProcessTime => groundedState.ProcessTime;

        readonly CharaControlTemplate controlFSM;
        readonly SamuraiGrounded_Katana groundedState;
        public SamuraiRunEnd_Katana(CharaControlTemplate controlFSM, SamuraiGrounded_Katana groundedState)
        {
            this.controlFSM = controlFSM;
            this.groundedState = groundedState;
        }

        public void Begin()
        {
            controlFSM.Chara.Animator.SetTrigger("RunEnd");
        }

        public bool Transite()
        {

            groundedState.Process();

            var state = controlFSM.Chara.Animator.GetCurrentAnimatorStateInfo(0);

            return state.IsName("Strafe_Run_F_End") && state.normalizedTime > 0.8f;
        }

    }
}