

using QS.Chara.Abilities;
using QS.Common.FSM;
using System.Collections;
using UnityEngine;

namespace QS.Chara
{
    public class SamuraiHit_Katana : CharaStateBase
    {
        public override CharaState State => CharaState.Hit;
        readonly Animator animator;
        public float hitStopTime = .2f;
        Coroutine coroutine;
        float recoveryTime = 1f;
        public SamuraiHit_Katana(CharaControlTemplate controlFSM) : base(controlFSM)
        {
            animator = controlFSM.GetComponent<Animator>();
        }

        public override void Enter()
        {
            recoveryTime = ControlFSM.hitStopTime;
            animator.SetBool("HitStop", true);
            animator.SetFloat("AttackForce", ControlFSM.AttackForce);
            var dir = AnimUtil.CrossInput2AttackDir(ControlFSM.AttackDir);
            //Debug.Log($"Attack Dir {dir}");
            animator.SetFloat("AttackDir", dir);
            coroutine = ControlFSM.StartCoroutine(Recovery());
        }

        public override void Exit()
        {
            animator.SetBool("HitStop", false);
            ControlFSM.StopCoroutine(coroutine);
        }

        public override ProcessTime ProcessTime => ProcessTime.AnimationMove;
        public override void Process()
        {
            ControlFSM.Motor.UnclampedDisplacement = animator.deltaPosition;
            //Debug.Log(ControlFSM.Motor.UnclampedDisplacement);
            ControlFSM.ApplyDefaultDosplacement();
        }

        IEnumerator Recovery()
        {
            // Ó²Ö±Ê±¼ä
            yield return new WaitForSeconds(recoveryTime);
            ControlFSM.SwitchTo(CharaState.Idle);
        }
    }
}