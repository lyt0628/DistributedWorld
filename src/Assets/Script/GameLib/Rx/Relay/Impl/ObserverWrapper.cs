


using System;

namespace QS.GameLib.Rx.Relay
{
    class ObserverWrapper<T> : IObserver<T>
    {
        readonly Action<T> onNext;
        readonly Action<Exception> onError;
        readonly Action onComplete;
        public ObserverWrapper(Action<T> onNext, Action<Exception> onError, Action onComplete)
        {
            this.onNext = onNext;
            this.onError = onError;
            this.onComplete = onComplete;
        }
        public ObserverWrapper(Action<T> onNext, Action onComplete)
        {
            this.onNext = onNext;
            this.onComplete = onComplete;
        }
        public ObserverWrapper(Action<T> onNext, Action<Exception> onError)
        {
            this.onNext = onNext;
            this.onError = onError;
        }

        public ObserverWrapper(Action<T> onNext)
        {
            this.onNext = onNext;
        }

        public void OnCompleted()
        {
            onComplete?.Invoke();
        }

        public void OnError(Exception exception)
        {
            onError?.Invoke(exception);
        }

        public void OnNext(T item)
        {
            onNext.Invoke(item);
        }

    }
}