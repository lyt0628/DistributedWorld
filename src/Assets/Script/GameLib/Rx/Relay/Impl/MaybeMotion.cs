

namespace QS.GameLib.Rx.Relay
{
    class MaybeMotion<T> : AbstractMotion<T>
    {
        public T Value { get; set; }
        public MaybeMotion(IObserver<T> observer, T value = default) : base(observer)
        {
            Value = value;
        }

        protected override void DoSet()
        {
            observer.OnNext(Value);
        }
    }
}