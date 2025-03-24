using QS.Api.Common;
using System;
using System.Threading.Tasks;

namespace QS.Common
{
    public interface IAsyncOp<T> : IAsyncOp
    {
        new IAsyncOpHandle<T> Handle { get; }
        T Result { get; }
        new Task<T> Task { get; }

        new event Action<IAsyncOpHandle<T>> Completed;
    }

    public interface IAsyncOp
    {
        bool CanInvoke { get; }
        IAsyncOpHandle Handle { get; }
        bool HasExecuted { get; }
        bool IsDone { get; }
        Task Task { get; }

        event Action Completed;

        void Invoke();
    }
}