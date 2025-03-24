
namespace QS.GameLib.Rx.Relay
{

    public interface IOperator<T, U> : IObserver<T>, IObservable<U>
    {

    }
}