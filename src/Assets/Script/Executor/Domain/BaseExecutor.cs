



using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Rx.Relay;

namespace QS.Api.Executor.Domain
{
    public class BaseExecutor : IRelayExecutor
    {

        readonly IPipelineContext _pipelineContext = IPipelineContext.New();

        public void AddAfter(string baseName, string name, IInstructionHandler handler)
        {
            _pipelineContext.Pipeline.AddAfter(baseName, name, handler);
        }

        public void AddBefore(string baseName, string name, IInstructionHandler handler)
        {
            _pipelineContext.Pipeline.AddBefore(baseName, name, handler);
        }

        public void AddFirst(string name, IInstructionHandler handler)
        {
            _pipelineContext.Pipeline.AddFirst(name, handler);
        }

        public void AddLast(string name, IInstructionHandler handler)
        {
            _pipelineContext.Pipeline.AddLast(name, handler);
        }

        public void Execute(Relay<IInstruction> instructions)
        {
            instructions.Subscrib(i=>Execute(i));
        }

        public void Execute(IInstruction instruction)
        {
            _pipelineContext.InBound(instruction);
        }

        public void Remove(string name)
        {
            _pipelineContext.Pipeline.Remove(name);
        }

        public void Remove(IInstructionHandler handler)
        {
            _pipelineContext.Pipeline.Remove(handler);
        }
    }
}