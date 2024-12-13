


using System;

namespace QS.GameLib.Rx.Relay
{
    abstract class AbstractObservable<T> : IObservable<T>, IMotionProvider
    {
        readonly MotionGroup motions = new();
        public IMotion Get()
        {
            return motions;
        }

        public void Subscribe(IObserver<T> observer)
        {
            try
            {
                var m = DoSubscribe(observer);
                motions.Add(m);
            }catch(Exception e)
            {
                observer.OnError(e);
            }
        }

        protected abstract IDisposableMotion DoSubscribe(IObserver<T> observer);
    }
}