

namespace QS.GameLib.Rx.Relay
{
    public interface IObservable<out T>
    {
        void Subscribe(IObserver<T> observer);
    }
}