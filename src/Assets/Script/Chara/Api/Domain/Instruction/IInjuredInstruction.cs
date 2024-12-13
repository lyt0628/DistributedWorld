

using QS.Api.Executor.Domain;

namespace QS.Api.Character.Instruction
{
    public interface IInjuredInstruction : IInstruction
    {
          float Atk { get; }
          float Matk { get; }
    }
}