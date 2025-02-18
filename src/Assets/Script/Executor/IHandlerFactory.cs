using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;
using QS.Executor.Domain.Handler;
using QS.Executor.Domain.Instruction;
using QS.GameLib.Pattern.Pipeline;
using UnityEngine;

namespace QS.Api.Executor.Service
{
    public interface IHandlerFactory
    {
   
        IInstructionHandler Instantiate(IRelayExecutor executor);
        IFilterHandler Filter(IRelayExecutor executor);
        IInstructionHandler Combat(IRelayExecutor executor, IBuffedCombater combator);

    }
}