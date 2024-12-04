


using System;

namespace QS.GameLib.Rx.Relay
{
    class TickMotion<T> : AbstractMotion<T>
    {
        readonly Func<T> supplier;
        public TickMotion(IObserver<T> observer, Func<T> supplier) 
            : base(observer)
        {
            this.supplier = supplier;
        }

        protected override void DoSet()
        {
            var t = supplier();
            observer.OnNext(t);
        }
    }
}