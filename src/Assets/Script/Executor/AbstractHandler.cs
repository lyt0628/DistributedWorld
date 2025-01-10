


using QS.Api.Executor.Domain;
using QS.GameLib.Pattern.Pipeline;
using UnityEngine;

namespace QS.Executor.Domain
{
    public abstract class AbstractHandler
        : InBoundPipelineHandlerAdapter, IInstructionHandler
    {
        public AbstractHandler(IRelayExecutor executor) 
        {
            Executor = executor;
        }

        public IRelayExecutor Executor { get; }
    }
}