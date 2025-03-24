

using QS.Chara.Abilities;
using QS.Combat;
using QS.Common.FSM;
using System.Collections;
using UnityEngine;

namespace QS.Chara
{
    /// <summary>
    /// 具体的角色控制状态不可能通用，换到别的包下
    /// </summary>
    class SamuraiDodge_Katana : CharaStateBase
    {
        readonly Animator animator;

        public SamuraiDodge_Katana(CharaControlTemplate controlFSM) : base(controlFSM)
        {
            animator = controlFSM.GetComponent<Animator>();

        }

        public override CharaState State => CharaState.Dodge;
        public override ProcessTime ProcessTime => ProcessTime.AnimationMove;
        public override void Process()
        {
            ControlFSM.Motor.UnclampedDisplacement = animator.deltaPosition;
            ControlFSM.ApplyDefaultDosplacement();
            //ControlFSM.ApplyDefaultRotation();
            ControlFSM.Chara.InstrFiler.AddToBlackList(typeof(IHitInstr), typeof(IAttackedInstr));
           
        }
        Coroutine m_rollCountDownRoutine;
        public override void Enter()
        {
            animator.SetTrigger("Roll");
            animator.SetFloat("MainMoveDir", AnimUtil.CountMainMoveDir(ControlFSM.LocalHorVelocity.normalized));
            m_rollCountDownRoutine = ControlFSM.StartCoroutine(RollTimeCountDown());
        }

        public override void Exit()
        {
            ControlFSM.StopCoroutine(m_rollCountDownRoutine);
            ControlFSM.Chara.InstrFiler.RemoveFromBlackList(typeof(IHitInstr), typeof(IAttackedInstr));
        }

        IEnumerator RollTimeCountDown()
        {

            yield return new WaitForSeconds(1f);
            ControlFSM.SwitchTo(CharaState.Idle);
        }
    }
}