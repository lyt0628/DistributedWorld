

using QS.Chara.Abilities;
using UnityEngine;

namespace QS.Chara
{

    class SamuraiJump_Katana : CharaStateBase
    {
        readonly Animator animator;
        public SamuraiJump_Katana(CharaControlTemplate controlFSM) : base(controlFSM)
        {
            animator = controlFSM.GetComponent<Animator>();
        }

        public override CharaState State => CharaState.Jumping;

        public override void Enter()
        {
            animator.SetBool("Jumping", true);
        }
        public override void Exit()
        {
            animator.SetBool("Jumping", false);
        }
    }
}