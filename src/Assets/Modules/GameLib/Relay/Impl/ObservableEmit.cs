


using System;
using System.Collections.Generic;

namespace QS.GameLib.Rx.Relay
{
    class ObservableEmit<T> : IObservable<T>
    {
        internal MyEmitter emitter;
        readonly List<IObserver<T>> observers = new();
        public ObservableEmit()
        {
            emitter = new(() => observers);
        }
        public void Subscribe(IObserver<T> observer)
        {

            observers.Add(observer);
        }

        internal class MyEmitter : IEmitter<T>
        {
            Func<List<IObserver<T>>> getObservers;
            public MyEmitter(Func<List<IObserver<T>>> getObservers)
            {
                this.getObservers = getObservers;
            }

            public void Emit(T obj)
            {
                var os = getObservers();
                try
                {

                    os.ForEach(o => o.OnNext(obj));
                }
                catch (Exception e)
                {
                    os.ForEach(o => o.OnError(e));
                }
            }
        }
    }
}