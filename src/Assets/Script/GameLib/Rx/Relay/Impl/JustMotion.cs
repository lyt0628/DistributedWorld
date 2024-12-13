

using System;
using System.Collections;
using System.Collections.Generic;

namespace QS.GameLib.Rx.Relay
{
    class JustMotion<T> : AbstractMotion<T>
    {
        readonly IEnumerable<T> objects;
        public JustMotion(IObserver<T> observer, IEnumerable<T> objects)
            : base(observer)
        {
            this.objects = objects;
        }


        protected override void DoSet()
        {
            foreach (var obj in objects)
            {
                observer.OnNext(obj);
            }
            observer.OnCompleted();
        }
    }
}