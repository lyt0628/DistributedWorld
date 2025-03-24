


using System;

namespace QS.GameLib.Rx.Relay
{
    abstract class AbstractMotion<T> : IDisposableMotion
    {
        protected IObserver<T> observer;
        public AbstractMotion(IObserver<T> observer)
        {
            this.observer = observer;
        }

        public bool Disposed { get; set; } = false;

        public bool Paused { get; set; } = false;


        public void Set()
        {
            try
            {
                DoSet();
            }
            catch (Exception e)
            {
                observer.OnError(e);
            }
        }

        protected abstract void DoSet();


    }
}