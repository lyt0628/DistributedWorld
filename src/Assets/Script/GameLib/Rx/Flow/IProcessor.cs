namespace QS.GameLib.Rx
{

    public interface IProcessor<T, U> : IPublisher<T>, ISubcscriber<U>
    {

    }
}