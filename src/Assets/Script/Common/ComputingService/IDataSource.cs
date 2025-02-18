


using QS.GameLib.Rx.Relay;

namespace QS.Common.ComputingService
{
    public interface IDataSource<T> where T : new()
    {

        //T Create();

        string Add(Relay<T> data);

        void Remove(string uuid);

    }
}