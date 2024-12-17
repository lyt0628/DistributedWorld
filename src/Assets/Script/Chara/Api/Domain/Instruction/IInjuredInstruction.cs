using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;

namespace QS.Api.Chara.Instruction
{
    public interface IInjuredInstruction : IInstruction
    {
        IAttack Attack { get; }
    }
}