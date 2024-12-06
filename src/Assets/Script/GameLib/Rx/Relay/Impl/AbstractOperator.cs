


using System;
using System.Collections.Generic;

namespace QS.GameLib.Rx.Relay
{
    abstract class AbstractOperator<T, U> : IOperator<T, U>
    {
        readonly List<IObserver<U>> observers = new();
        public void OnCompleted()
        {
            observers.ForEach(x => x.OnCompleted());
            observers.Clear();
        }
        public void OnError(Exception exception)
        {
            observers.ForEach(observer => observer.OnError(exception));
        }
        public void OnNext(T t)
        {
            U u = Operate(t);
            observers.ForEach(x => x.OnNext(u));
        }


        public void Subscribe(IObserver<U> observer)
        {
            try
            {
                observers.Add(observer);
            }catch(Exception e)
            {
                observer.OnError(e);
            }

        }

        protected abstract U Operate(T t);

    }
}