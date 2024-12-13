


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

        bool _isDisposed = false;
        public bool IsDisposed => _isDisposed;
        
        

        public void Dispose()
        {
            _isDisposed = true;
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