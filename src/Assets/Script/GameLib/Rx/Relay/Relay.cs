

using System;
using System.Collections.Generic;

namespace QS.GameLib.Rx.Relay
{
    public class Relay<T>
    {
        protected Relay(IObservable<T> observable) 
        {
            this.observable = observable;
        }
        protected readonly IObservable<T> observable;
        public static Relay<T> Of(IObservable<T> observable)
        {
            return new Relay<T>(observable);
        }

        public static Relay<T> Just(IEnumerable<T> values)
        {
            var observable = new ObservableJust<T>(values);
            return new Relay<T>(observable);
        }

        public static Relay<T> Tick(Func<T> supplier, out IMotion motion)
        {
            var o = new ObservableTick<T>(supplier);
            motion = o.Get();
            return new Relay<T>(o);
        }

        public static Relay<T> CombineLatest(
            IEnumerable<IObservable<object>> observables,
            Func<IEnumerable<object>, T> combiner)
        {
            var o = new ObservableCombineLatest<T>(observables,combiner);
            return new Relay<T>(o);
        }

        public Relay<U> Map <U> (Func<T,U> mapper)
        {
            var op = new MapOperator<T,U>(mapper);
            observable.Subscribe(op);
            return new Relay<U>(op);
        }

        public Relay<U> Operate<U>(IOperator<T, U> op)
        {
            observable.Subscribe(op);
            return new Relay<U>(op);
        }

        public Relay<T> Subscrib(Action<T> onNext)
        {
            var observer = new ObserverWrapper<T>(onNext);
            observable.Subscribe(observer);
            return this;
        }

        public Relay<T> Subscrib(Action<T> onNext, out IDisposable disposable)
        {
            var observer = new ObserverWrapper<T>(onNext);
            observable.Subscribe(observer);
            disposable = observer.Disposable;
            return this;
        }

        public Relay<T> Subscrib(Action<T> onNext, Action<Exception> onError)
        {
            var observer = new ObserverWrapper<T>(onNext, onError);
            observable.Subscribe(observer);
            return this;
        }
        public Relay<T> Subscrib(Action<T> onNext, Action<Exception> onError, out IDisposable disposable)
        {
            var observer = new ObserverWrapper<T>(onNext, onError);
            observable.Subscribe(observer);
            disposable = observer.Disposable;
            return this;
        }

        public Relay<T> Subscrib(Action<T> onNext, Action onComplete)
        {
            var observer = new ObserverWrapper<T>(onNext, onComplete);
            observable.Subscribe(observer);
            return this;
        }

        public Relay<T> Subscrib(Action<T> onNext, Action onComplete, out IDisposable disposable)
        {
            var observer = new ObserverWrapper<T>(onNext, onComplete);
            observable.Subscribe(observer);
            disposable = observer.Disposable;
            return this;
        }

    }
}