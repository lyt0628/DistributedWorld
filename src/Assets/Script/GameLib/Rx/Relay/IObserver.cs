

using System;

namespace QS.GameLib.Rx.Relay
{
    public interface IObserver<T>
    {
        void OnNext(T item);
        void OnError(Exception exception);
        void OnCompleted();

    }
}