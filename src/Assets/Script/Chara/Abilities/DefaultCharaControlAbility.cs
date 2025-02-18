

using QS.Api.Executor.Domain.Instruction;
using QS.Chara.Domain;

namespace QS.Chara.Abilities
{
    class DefaultCharaControlAbility : BaseCharaControlAbilityWrapper
    {
        readonly BaseCharaControlState idleControl;
        readonly BaseCharaControlState moveControl;
        readonly BaseCharaControlState rollControl;
        public DefaultCharaControlAbility(Character character) : base(character)
        {
            idleControl = new CharaControlIdle(character, this);
            moveControl = new CharaControlMove(character, this);
            rollControl = new CharaControlRoll(character, this);
        }

        protected override bool DoSwitchTo(CharaControlState to, CharaControlState from, out BaseCharaControlState state)
        {
            if (to is CharaControlState.Idle)
            {
                state = idleControl;
                return true;
            }
            state = default;
            return false;
        }

        protected override BaseCharaControlState GetControl(BaseCharaControlState currentState, ICharaControlInstr instr, bool IsGrounded)
        {
            if (currentState.State == CharaControlState.Roll)
            {
                return currentState;
            }

            if (instr.Horizontal == 0 && instr.Vertical == 0)
            {
                return idleControl;
            }

            if(currentControl.State is CharaControlState.Walking or CharaControlState.Runing &&
                instr.Crouch)
            {
                return rollControl;
            }
            return moveControl;
        }
    }
}