

using QS.Api.Executor.Domain;
using QS.Chara.Abilities;
using QS.Common.FSM;
using QS.Common.Util;
using UnityEngine;

namespace QS.Chara
{
    public class SamuraiFSM : CharaControlTemplate
    {
        public int currentWeapon = 1;

        CharaStateBase rootMotionControl;
        #region [[Katana]]
        CharaStateBase katanaMove;
        CharaStateBase katanaHit;
        CharaStateBase kanataDodge;
        CharaStateBase katanaJump;
        ITransition<CharaState> katana2Bow;
        #endregion

        #region [[Bow]]
        CharaStateBase bowMove;
        #endregion

        protected override void Start()
        {
            base.Start();

            Motor = new DefaultCharaControlMotor(this);
            PhysicalProbe = new DefaultCharaControlPhysicalProbe(this);
            katanaHit = new SamuraiHit_Katana(this);
            katanaMove = new SamuraiGrounded_Katana(this);
            rootMotionControl = new DefaultRootMotionState(this);
            kanataDodge = new SamuraiDodge_Katana(this);
            katanaJump = new SamuraiJump_Katana(this);

            katana2Bow = new SamuraiTransitionKatana2Bow(this);
            bowMove = new SamuraiMove_Bow(this);
        }

        public override bool CanHandle(IInstruction instruction) => currentWeapon switch
        {
            1 => instruction is IMoveInstr or IAimLockInstr or IHitInstr
                                                    or IDodgeInstr or IJumpInstr
                                                    or SwitchWeaponInstr,
            2 => instruction is IMoveInstr,
            _ => false,
        };

        public override CharaStateBase GetCharaState(CharaState state)
        {
            return currentWeapon switch
            {
                1 => state switch
                {
                    CharaState.Idle => katanaMove,
                    CharaState.Walking => katanaMove,
                    CharaState.Runing => katanaMove,
                    CharaState.RootMotion => rootMotionControl,
                    CharaState.Hit => katanaHit,
                    CharaState.Dodge => kanataDodge,
                    CharaState.Jumping => katanaJump,
                    _ => throw new System.NotImplementedException(),
                },
                2 => bowMove,
                _ => throw new System.NotImplementedException(),
            };
        }


        public override void Handle(IInstruction instruction)
        {
            base.Handle(instruction);

            if (instruction is SwitchWeaponInstr weaponInstr)
            {
                if (CurrentState is CharaState.Idle or CharaState.Walking or CharaState.Runing)
                {
                    currentWeapon = 2;
                    ForceTransite(katana2Bow);
                }
                else
                {
                    return;
                }
            }
        }

    }
}