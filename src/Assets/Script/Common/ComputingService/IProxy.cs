

namespace QS.Common.ComputingService
{
    public interface IProxy<T> where T : ISnapshot
    {
        T Snapshot { get; }

        public void UpdateSnapshot(T snapshot);
    }
}