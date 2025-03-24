

using QS.Chara.Abilities;
using QS.Common.FSM;
using System;
using UnityEngine;

namespace QS.Chara
{
    class SamuraiRunStart_Katana : ICharaStateTransition
    {
        public Func<bool> Condition => () =>
        {
            return controlFSM.Run && controlFSM.CurrentState is CharaState.Idle
            && !Mathf.Approximately(controlFSM.MoveDir.magnitude, 0)
            && (!controlFSM.AimLock || AnimUtil.CountMainMoveDir(controlFSM.LocalHorVelocity) == AnimUtil.RELATIVE_DIR_F);
        };

        readonly CharaControlTemplate controlFSM;
        readonly SamuraiGrounded_Katana groundedState;
        public SamuraiRunStart_Katana(CharaControlTemplate controlFSM, SamuraiGrounded_Katana groundedState)
        {
            this.controlFSM = controlFSM;
            this.groundedState = groundedState;
        }

        public CharaState Target => CharaState.Runing;

        public ProcessTime ProcessTime => groundedState.ProcessTime;

        public void Begin()
        {
            controlFSM.Chara.Animator.SetTrigger("RunStart");
        }

        public bool Transite()
        {
            
            groundedState.Process();
            var state = controlFSM.Chara.Animator.GetCurrentAnimatorStateInfo(0);

            return state.IsName("Strafe_Run_F_Start") && state.normalizedTime > 0.8f;
        }
    }
}