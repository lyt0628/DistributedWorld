

using QS.GameLib.Pattern.Pipeline;

namespace QS.Api.Executor.Domain
{
    public interface IInstructionHandler : IPipelineHandler
    {
        IRelayExecutor Executor { get; }
    }
}