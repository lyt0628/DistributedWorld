namespace QS.GameLib.Pattern
{

    public interface IPublisher<T>
    {
        public void Subscribe(ISubcscriber<T> subcscriber);
        public void Submit(T item);
    }
}
