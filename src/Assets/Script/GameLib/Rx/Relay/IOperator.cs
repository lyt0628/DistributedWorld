
namespace QS.GameLib.Rx.Relay
{
    
    public interface IOperator<T, U> : IObservable<T>, IObserver<U>
    {

    }
}