




using GameLib.DI;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;
using QS.Api.Executor.Service;
using QS.Executor.Domain.Handler;
using QS.Executor.Domain.Instruction;
using QS.GameLib.Pattern.Pipeline;
using UnityEngine;

namespace QS.Executor.Service
{
    class InstructionHandlerFactory : IInstructionHandlerFactory
    {
        public IInstructionHandler Instantiate(IRelayExecutor executor)
        {

            return new InstantiateInstructionHandler(executor);
        }

        public IInstructionHandler Move(IRelayExecutor executor, Transform transform, Animator animator)
        {
            return new MoveInstructionHandler(executor ,transform, animator);
        }
    }
}