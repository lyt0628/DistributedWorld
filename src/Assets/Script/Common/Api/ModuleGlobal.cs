



using GameLib.DI;
using QS.Common;
using QS.GameLib.Pattern;

namespace QS.Api.Common
{
    public abstract class ModuleGlobal<T>
        : Sington<T>, IBindingProvider, IInstanceProvider where T : new()
    {
        protected abstract IDIContext DIContext {get;}

        public R GetInstance<R>()
        {
            return DIContext.GetInstance<R>();
        }

        public R GetInstance<R>(string name)
        {
            return DIContext.GetInstance<R>(name);
        }

        public abstract void ProvideBinding(IDIContext context);

    }
}