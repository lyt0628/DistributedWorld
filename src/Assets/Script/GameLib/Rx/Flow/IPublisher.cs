namespace QS.GameLib.Rx
{

    public interface IPublisher<T>
    {
        void Subscribe(ISubcscriber<T> subcscriber);
        void Submit(T item);
    }
}
