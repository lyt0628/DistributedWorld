




using GameLib.DI;
using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;
using QS.Api.Executor.Service;
using QS.Chara.Domain.Handler;
using QS.Executor.Domain.Handler;
using QS.Executor.Domain.Instruction;
using QS.GameLib.Pattern.Pipeline;
using UnityEngine;

namespace QS.Executor.Service
{
    class InstructionHandlerFactory : IHandlerFactory
    {
        public IFilterHandler Filter(IRelayExecutor executor)
        {
            return new FilterHandler(executor);
        }

        public IInstructionHandler Instantiate(IRelayExecutor executor)
        {

            return new InstantiateInstructionHandler(executor);
        }
        public IInstructionHandler Combat(IRelayExecutor executor, IBuffedCombater combator)
        {
            var h = new CombatorHandler(executor, combator);
            ExecutorGlobal.Instance.DI.Inject(h);
            return h;
        }

    }
}