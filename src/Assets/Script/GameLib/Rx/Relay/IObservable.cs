

namespace QS.GameLib.Rx.Relay
{
    public interface IObservable<T>
    {
        void Subscribe(IObserver<T> observer);
    }
}