namespace QS.GameLib.Pattern
{
    public interface ISubscription
    {
        void Request(long n);
        void Cancel();
    }
}
