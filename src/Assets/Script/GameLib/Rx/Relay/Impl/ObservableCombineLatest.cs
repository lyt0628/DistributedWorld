


using System;
using System.Collections.Generic;
using System.Linq;

namespace QS.GameLib.Rx.Relay
{
    class ObservableCombineLatest<T> : AbstractObservable<T>
    {
        readonly List<MaybeMotion<T>> motions = new();
        readonly List<object> products = new();
        readonly int count;
        int ready = 0;

        public ObservableCombineLatest(
            IEnumerable<IObservable<object>> observables,
            Func<IEnumerable<object>, T> combiner)
        {
            count = observables.Count();
            var i = 0;
            foreach (var observable in observables)
            {
                observable.Subscribe(new ObserverWrapper<object>((o) => 
                {
                    if(products[i] == null)
                    {
                        ready++;
                    }
                    products[i] = o;

                    if(ready == count)
                    {
                        var t = combiner(products);
                        motions.ForEach(m => m.Value = t);
                        Get().Set();

                        ready = 0;
                        products.Clear();
                    }
                }));

                i++;
            }
        }

         protected override IDisposableMotion DoSubscribe(IObserver<T> observer)
        {
            var motion = new MaybeMotion<T>(observer);
            motions.Add(motion);
            return motion;
        }
    }
}