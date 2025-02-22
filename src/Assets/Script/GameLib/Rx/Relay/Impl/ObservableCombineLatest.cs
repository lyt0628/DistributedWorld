


using System;
using System.Collections.Generic;
using System.Linq;

namespace QS.GameLib.Rx.Relay
{
    class ObservableCombineLatest<T> : AbstractObservable<T>
    {
        readonly Dictionary<object, object> dict = new();
        T value;
        public ObservableCombineLatest(
            IEnumerable<IObservable<object>> observables,
            Func<IEnumerable<object>, T> combiner)
        {

            foreach (var o in observables)
            {
                o.Subscribe(new ObserverWrapper<object>((p) =>
                {
                    dict[o] = p;
                    if(dict.Count() == observables.Count())
                    {
                        value = combiner(dict.Values);
                        dict.Clear();
                        Get().Set();
                    }
                }));
            }

        }

         protected override IDisposableMotion DoSubscribe(IObserver<T> observer)
        {
            return new TickMotion<T>(observer, () => value);
        }
    }
}