

namespace GameLib
{
    public interface ISubscription
    {
        void Request(long n);
        void Cancel();
    }
}
