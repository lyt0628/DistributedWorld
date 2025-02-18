

using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;
using QS.Chara.Domain;
using QS.Executor.Domain;
using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Util;

namespace QS.Chara.Abilities
{
    abstract class AbstractCharaControlAbility : Ability, ICharaConrolAbility
    {
        protected AbstractCharaControlAbility(Character character) : base(character)
        {
        }

        public bool Enabled { get; set; } = true;

        public abstract CharaControlState State { get; }
        public abstract void OnControl(ICharaControlInstr instr);
        public abstract void OnFrozenControl(ICharaControlInstr instr);

        public override void Read(IPipelineHandlerContext context, object msg)
        {
            if (msg is not ICharaControlInstr instr)
            {
                context.Write(msg);
                return;
            }
            
            if (Enabled)
            {
                OnControl(instr);
            }
            else
            {
                OnFrozenControl(instr);
            }
        }

    }
}