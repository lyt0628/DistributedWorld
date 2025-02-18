

namespace QS.Common.ComputingService
{
    /// <summary>
    ///  这个类实际上只是维护了一个状态，
    ///  或许简单使用一个字典存储会更好，因为现在的泛型太复杂了
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseProxy<T> : IProxy<T> where T : ISnapshot
    {

        public void UpdateSnapshot(T snapshot)
        {
            Snapshot = snapshot;
        }

        public T Snapshot { get; private set; }
    }
}