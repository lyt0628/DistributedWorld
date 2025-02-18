

using System;

namespace QS.Api.Executor.Domain
{
    public interface IFilterHandler : IInstructionHandler
    {
        void AddToBlackList<T>() where T : IInstruction;
        void RemoveFromBlackList<T>() where T : IInstruction;
        void AddBlockCondition(string id, Predicate<object> condition);
        void RemoveBlockCondition(string id);

    }
}