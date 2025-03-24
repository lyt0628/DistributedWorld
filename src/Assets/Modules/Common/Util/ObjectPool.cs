using System;
using System.Collections.Generic;

namespace QS.Common.Util
{
    public class ObjectPool : ObjectPool<object>
    {
        public ObjectPool(Func<object> supper, int maxSize = 50) : base(supper, maxSize)
        {
        }
    }

    public class ObjectPool<T>
    {
        readonly Stack<T> stack = new();
        readonly Func<T> supper;
        readonly int maxSize;

        public ObjectPool(Func<T> supper, int maxSize = 50)
        {
            this.supper = supper;
            this.maxSize = maxSize;
        }

        public T Get()
        {
            if (stack.TryPop(out var obj))
            {
                return obj;
            }
            return supper();
        }

        public void Release(T obj)
        {
            if (stack.Count <= maxSize)
            {
                stack.Push(obj);
            }

        }
    }
}