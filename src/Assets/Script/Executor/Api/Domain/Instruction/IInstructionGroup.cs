

namespace QS.Api.Executor.Domain.Instruction
{
    public interface IInstructionGroup : IInstruction
    {
        IInstruction[] Instructions { get; }

    }
}