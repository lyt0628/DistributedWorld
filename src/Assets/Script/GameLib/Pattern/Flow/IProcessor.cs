

namespace GameLib
{

    public interface IProcessor<T, U> : IPublisher<T>, ISubcscriber<U>
    {

    }
}