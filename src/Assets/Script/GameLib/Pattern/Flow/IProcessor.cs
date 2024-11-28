namespace QS.GameLib.Pattern
{

    public interface IProcessor<T, U> : IPublisher<T>, ISubcscriber<U>
    {

    }
}