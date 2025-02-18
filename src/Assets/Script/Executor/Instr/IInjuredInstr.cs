using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;

namespace QS.Api.Chara.Instruction
{
    public interface IInjuredInstr : IInstruction
    {
        IAttack Attack { get; }
    }
}