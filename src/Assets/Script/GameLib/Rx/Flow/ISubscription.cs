namespace QS.GameLib.Rx
{
    public interface ISubscription
    {
        void Request(long n);
        void Cancel();
    }
}
