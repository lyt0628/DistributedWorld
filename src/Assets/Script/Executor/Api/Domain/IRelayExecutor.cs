

using QS.GameLib.Rx.Relay;

namespace QS.Api.Executor.Domain
{
    /// <summary>
    /// ��ߵ���Ӧ��Ҫ���б�ѹ, �������ʹ��ͬ����
    /// </summary>
    public interface IRelayExecutor : IExecutor, IInstructionPipeline
    {
        void Execute(Relay<IInstruction> instructions);
    }
}
