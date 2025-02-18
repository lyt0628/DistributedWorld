


using GameLib.DI;
using QS.Api.Executor.Domain.Instruction;
using QS.Api.Setting;
using QS.Chara.Domain;
using QS.Common.ComputingService;
using QS.GameLib.Uitl.RayCast;
using QS.GameLib.Util.Raycast;
using UnityEngine;
using UnityEngine.Assertions;

namespace QS.Chara.Abilities
{
     abstract class BaseCharaControlAbilityWrapper : AbstractCharaControlAbility
    {
        [Injected]
        readonly IGlobalPhysicSetting globalPhysic;

        public BaseCharaControlAbilityWrapper(Character character) : base(character)
        {
            currentControl = new CharaControlIdle(character, this);
        }

        protected abstract BaseCharaControlState GetControl(BaseCharaControlState oldState, ICharaControlInstr instr, bool IsGrounded);

        public bool SwitchTo(CharaControlState to, BaseCharaControlState from)
        {
            if(DoSwitchTo(to, from.State, out var state))
            {
                from.OnExit();
                currentControl = state;
                state.OnEnter();
                return true;
            }
            return false;
        }
        protected abstract bool DoSwitchTo(CharaControlState to, CharaControlState from, out BaseCharaControlState state);


        protected BaseCharaControlState currentControl;
        public override CharaControlState State => currentControl.State;
        protected bool IsGrounded
        {
            get
            {
                
                var downRay = RaycastHelper
                            .Of(CastedObject
                                    .Ray(Character.transform.position, Vector3.down)
                                    .IgnoreTrigger());
                return downRay.IsCloserThan(globalPhysic.ErrorTolerance);
            }
        }

        public override void OnControl(ICharaControlInstr instr)
        {
        
            BaseCharaControlState control = GetControl0(instr);
            control.OnControl(instr);
        }

        private BaseCharaControlState GetControl0(ICharaControlInstr instr)
        {
            var control = GetControl(currentControl, instr, IsGrounded);
            if (currentControl.State != control.State)
            {
                currentControl.OnExit();
                currentControl = control;
                currentControl.OnEnter();
            }

            return control;
        }

        public override void OnFrozenControl(ICharaControlInstr instr)
        {

            BaseCharaControlState control = GetControl0(instr);
            control.OnFrozenControl(instr);
        }
    }
}