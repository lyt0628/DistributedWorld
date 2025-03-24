

using System.Collections;
using System.Collections.Generic;

namespace QS.Common
{
    public class LoopQueue<T> : ILoopQueue<T>
    {
        readonly T[] m_pool;

        public LoopQueue(int capacity = 5, bool overwritalbe = false)
        {
            Capacity = capacity;
            this.overwritalbe = overwritalbe;
            m_pool = new T[Capacity + 1];
        }


        public bool overwritalbe = false;
        public int Capacity { get; } = 5;
        public int Count 
        {
            get
            {
                if (end >= start) return end - start;
                return Capacity - (start - end) + 1;

            }
        }

        // ×ó±ÕÓÒ¿ª
        int start = 0;
        int end = 0;

        public void Clear()
        {
            start = end = 0;
        }

        public void Clear(T obj)
        {
            Clear();
            while(Count < Capacity)
            {
                Push(obj);
            }
        }

        class LoopQueueEnumrator : IEnumerator<T>
        {
            readonly LoopQueue<T> m_Queue;

            int index = -1;

            public LoopQueueEnumrator(LoopQueue<T> queue)
            {
                this.m_Queue = queue;
            }

            public T Current => m_Queue.m_pool[index];

            object IEnumerator.Current => m_Queue.m_pool[index];

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if(index == -1)
                {
                    index = m_Queue.start;
                }
                if (index == m_Queue.end)
                {
                    return false;
                }
                index++;
                index %= m_Queue.Capacity + 1;
                return true;
            }

            public void Reset()
            {
                index = -1;
            }
        }


        public IEnumerator<T> GetEnumerator()
        {
            return new LoopQueueEnumrator(this);
        }

        public T Pop()
        {
            if(Count == 0)
            {
                throw new System.InvalidOperationException("Empty");
            }
            var result = m_pool[start];
            start++;
            start %= Capacity + 1;

            return result;
        }

        public void Push(T obj)
        {
            if (Count == Capacity)
            {
                if (overwritalbe)
                {
                    Pop();
                }
                else
                {
                    throw new System.InvalidOperationException("No room to push");
                }
            }
            m_pool[end] = obj;
            end++;
            end %= Capacity + 1;


        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LoopQueueEnumrator(this);
        }
    }
}