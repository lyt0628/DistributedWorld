using QS.Api.Common;
using System;
using System.Threading.Tasks;

namespace QS.Common
{
    public abstract class AsyncOpBase<T> : IAsyncOp<T>
    {
        readonly TaskCompletionSource<T> m_TaskCompletionSource = new();
        public IAsyncOpHandle<T> Handle { get; }

        protected AsyncOpBase()
        {
            Handle = new AsyncOpHandle<T>(this);
            Task = m_TaskCompletionSource.Task;
        }

        protected void Complete(T result)
        {
            m_TaskCompletionSource.SetResult(result);
            Completed?.Invoke(Handle);
            TypelessCompleted?.Invoke();
        }

        protected abstract void Execute();

        /// <summary>
        /// 本身不会开新线程进行执行，想要在新线程执行，客户端必须自己做
        /// </summary>
        public void Invoke()
        {
            Execute();
            HasExecuted = true;
        }
        public bool HasExecuted { get; private set; }

        public Task<T> Task { get; }
        /// <summary>
        /// 直接使用这个Result会导致阻塞
        /// </summary>
        public T Result => Task.Result;
        public bool IsDone => Task.IsCompleted;
        public bool CanInvoke => !HasExecuted;

        IAsyncOpHandle IAsyncOp.Handle => throw new NotImplementedException();

        Task IAsyncOp.Task => throw new NotImplementedException();

        public event Action<IAsyncOpHandle<T>> Completed;

        private event Action TypelessCompleted;
        event Action IAsyncOp.Completed
        {
            add
            {
                TypelessCompleted += value;
            }

            remove
            {
                TypelessCompleted -= value;
            }
        }
    }
}