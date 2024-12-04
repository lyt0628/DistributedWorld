


using System;

namespace QS.GameLib.Rx.Relay
{
    abstract class AbstractMotion<T> : IMotion
    {
        protected IObserver<T> observer;
        public AbstractMotion(IObserver<T> observer)
        {
            this.observer = observer;
        }

        public void Set()
        {
            try
            {
                DoSet();
            }catch(Exception e)
            {
                observer.OnError(e);
            }
        }

        protected abstract void DoSet();
    }
}