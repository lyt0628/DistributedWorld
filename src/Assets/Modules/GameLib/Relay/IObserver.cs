

using System;

namespace QS.GameLib.Rx.Relay
{
    public interface IObserver<in T>
    {
        void OnSubscribe(IDisposable disposable);
        void OnNext(T item);
        void OnError(Exception exception);
        void OnCompleted();

    }
}