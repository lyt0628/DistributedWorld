




using System.Collections.Generic;

namespace QS.GameLib.Rx.Relay
{
    class ObservableJust<T> : AbstractObservable<T>
    {
        readonly IEnumerable<T> values;
        public ObservableJust(IEnumerable<T> values)
        {
            this.values = values;
        }

        protected override IDisposableMotion DoSubscribe(IObserver<T> observer)
        {
            var motion = new JustMotion<T>(observer, values);
            motion.Set();

            return motion;
        }
    }
}