

using QS.GameLib.Rx.Relay;

namespace QS.Common.ComputingService
{
    public interface IComputingService<T>
    {
        Relay<T> Get(string uuid);
    }
}