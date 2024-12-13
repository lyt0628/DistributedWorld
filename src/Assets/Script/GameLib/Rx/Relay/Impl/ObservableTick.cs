


using System;

namespace QS.GameLib.Rx.Relay
{
    class ObservableTick<T> : AbstractObservable<T>, IMotionProvider
    {
        readonly Func<T> supplier;
        public ObservableTick(Func<T> supplier) 
        {
            this.supplier = supplier;
        }


        protected override IDisposableMotion DoSubscribe(IObserver<T> observer)
        {
            return new TickMotion<T>(observer, supplier);
        }
    }
}