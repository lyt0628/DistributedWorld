

using QS.Api.Executor.Domain;
using System;

namespace QS.Common
{
    public interface IInstructionFiler
    {
        void AddToBlackList(params Type[] instrType);
        void AddToBlackList<T>() where T : IInstruction;
        void RemoveFromBlackList(params Type[] instrType);
        void RemoveFromBlackList<T>() where T : IInstruction;
    }
}