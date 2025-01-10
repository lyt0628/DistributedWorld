

namespace QS.Api.Common
{
    public interface IResourceInitializer
    {
        void Initialize();
        ResourceInitStatus ResourceStatus { get; }
    }
}