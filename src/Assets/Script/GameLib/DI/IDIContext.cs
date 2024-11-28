
using System;

namespace GameLib.DI
{
    public interface IDIContext
    {
        static IDIContext New()
        {
            var ctx = new DefaultDIContext();
            ctx.Bind<DefaultDIContext>();
            return ctx;
        }

        IDIContext Bind<T>(ScopeFlag scope = ScopeFlag.Default);
        IDIContext Bind<T>(string name, ScopeFlag scope = ScopeFlag.Default);
        IDIContext BindInstance(Type target, object instance);
        IDIContext BindInstance(object instance);
        IDIContext BindInstance(string name, object instance);
        IDIContext BindInstance(Key target, object instance);
        IDIContext BindInstance(string name, Type type, object instance);

        T GetInstance<T>();
        T GetInstance<T>(string name);


        /// <summary>
        /// Inject into target, but not bind it to DIContext.
        /// All MonoBehaviour should get injected, using this method.
        /// </summary>
        /// <typeparam name="T">Type of target</typeparam>
        /// <param name="target">object you want get injected</param>
        /// <returns></returns>
        IDIContext Inject(object target);
    }
}