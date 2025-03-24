

using UnityEngine.Events;

namespace QS.Common.Util
{
    public interface ITimer
    {
        void Clear();
        void Set(float interval = 1, float delay = 0);
        UnityEvent OnTick { get; }

    }
}