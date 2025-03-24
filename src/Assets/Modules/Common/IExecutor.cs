using QS.Api.Executor.Domain;

namespace QS.Common
{

    public interface IExecutor
    {
        /// <summary>
        /// Invoke instruction
        /// </summary>
        /// <param name="instruction">The instruction to be executed</param>
        void Execute(IInstruction instruction);
    }
}
