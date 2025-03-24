using QS.Common;
using System;
using System.Threading.Tasks;

namespace QS.Api.Common
{
    public class AsyncOpHandle<T> : IAsyncOpHandle<T>
    {
        internal readonly AsyncOpBase<T> m_AsyncOp;
        public AsyncOpHandle(AsyncOpBase<T> asyncOp)
        {
            m_AsyncOp = asyncOp;
        }

        public Task<T> Task => m_AsyncOp.Task;
        public bool IsDone => m_AsyncOp.IsDone;



        public T Result => m_AsyncOp.Result;

        Task IAsyncOpHandle.Task => m_AsyncOp.Task;

        public event Action<IAsyncOpHandle<T>> OnCompleted
        {
            add { m_AsyncOp.Completed += value; }
            remove { m_AsyncOp.Completed -= value; }
        }

        event Action IAsyncOpHandle.OnCompleted
        {
            add
            {
                ((IAsyncOp)m_AsyncOp).Completed += value;
            }

            remove
            {
                ((IAsyncOp)m_AsyncOp).Completed -= value;
            }
        }
    }
}