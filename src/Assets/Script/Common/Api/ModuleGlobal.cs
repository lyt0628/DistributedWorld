



using GameLib.DI;
using QS.Common;
using QS.GameLib.Pattern;

namespace QS.Api.Common
{
    public abstract class ModuleGlobal<T>
        : Sington<T>, IResourceInitializer , IBindingProvider, IInstanceProvider where T : new()
    {
        public ResourceInitStatus ResourceStatus { get; protected set; } = ResourceInitStatus.Initializing;

        protected abstract IDIContext DIContext {get;}

        public R GetInstance<R>()
        {
            return DIContext.GetInstance<R>();
        }

        public R GetInstance<R>(string name)
        {
            return DIContext.GetInstance<R>(name);
        }

        public virtual void Initialize()
        {
            ResourceStatus = ResourceInitStatus.Started;
        }

        public abstract void ProvideBinding(IDIContext context);

    }
}