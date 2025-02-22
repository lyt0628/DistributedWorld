

using UnityEngine.Events;

namespace QS.Api.Common
{
    public interface IResourceInitializer
    {
        void Initialize();
        ResourceInitStatus ResourceStatus { get; }
        UnityEvent OnReady { get; }
    }
}