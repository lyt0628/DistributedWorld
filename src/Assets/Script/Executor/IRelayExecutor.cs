

using QS.GameLib.Rx.Relay;

namespace QS.Api.Executor.Domain
{
    /// <summary>
    /// 这边的流应该要进行背压, 这边暂且使用同步流
    /// </summary>
    public interface IRelayExecutor : IExecutor, IInstructionPipeline
    {
        void Execute(Relay<IInstruction> instructions);
    }
}
