

namespace GameLib
{
    using System;
    public interface ISubcscriber<T>
    {
        void OnSubscribe(ISubscription subscription);
        void OnNext(T item);
        void OnError(Exception exception);
        void OnCompleted();
    }
}