
using System;

namespace GameLib.DI
{
    public interface IDIContext : IHierarchyDI
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

        IDIContext BindExternalInstance(Type target, object instance);
        IDIContext BindExternalInstance(object instance);
        IDIContext BindExternalInstance(string name, object instance);
        IDIContext BindExternalInstance(Key target, object instance);
        IDIContext BindExternalInstance(string name, Type type, object instance);

        T GetInstance<T>();
        object GetInstance(Type type);
        T GetInstance<T>(string name);
        object GetInstance(string name, Type type);
        object GetInstance(string name);
        bool TryGetInstance<T>(string name, out T value);

        /// <summary>
        /// Inject into source, but not bind it to DIContext.
        /// All MonoBehaviour should get injected, using this method.
        /// </summary>
        /// <typeparam name="T">Type of source</typeparam>
        /// <param name="target">object you want get injected</param>
        /// <returns></returns>
        IDIContext Inject(object target);
    }
}