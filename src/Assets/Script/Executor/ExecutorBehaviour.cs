

using QS.Api.Executor.Domain;
using QS.GameLib.Rx.Relay;
using UnityEngine;

namespace QS.Executor
{
    public class ExecutorBehaviour : MonoBehaviour, IRelayExecutor
    {

        readonly IRelayExecutor executor = new BaseExecutor();

        public void AddAfter(string baseName, string name, IInstructionHandler handler)
        {
            executor.AddAfter(baseName, name, handler);
        }

        public void AddBefore(string baseName, string name, IInstructionHandler handler)
        {
            executor.AddBefore(baseName, name, handler);
        }

        public void AddFirst(string name, IInstructionHandler handler)
        {
            executor.AddFirst(name, handler);
        }

        public void AddLast(string name, IInstructionHandler handler)
        {
            executor.AddLast(name, handler);
        }

        public void Execute(Relay<IInstruction> instructions)
        {
            executor.Execute(instructions);
        }

        public void Execute(IInstruction instruction)
        {
            executor.Execute(instruction);
        }

        public void Remove(string name)
        {
            executor.Remove(name);
        }

        public void Remove(IInstructionHandler handler)
        {
            executor.Remove(handler);
        }
    }
}