

using QS.Api.Executor.Domain.Instruction;
using QS.Chara.Domain;

namespace QS.Chara.Abilities
{
    abstract class BaseCharaControlState : AbstractCharaControlAbility
    {
        protected BaseCharaControlAbilityWrapper wrapper;
        public BaseCharaControlState(Character character, BaseCharaControlAbilityWrapper wrapper) : base(character)
        {
            this.wrapper = wrapper;
        }

        public virtual void OnExit()
        {

        }

        public virtual void OnEnter()
        {

        }
    }
}