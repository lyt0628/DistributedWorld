
using System.Collections.Generic;

namespace QS.Common
{

    public interface ILoopQueue<T> : IEnumerable<T>
    {
        int Capacity { get; }
        void Push(T obj);
        T Pop();
        void Clear();
        void Clear(T obj);
    }
}