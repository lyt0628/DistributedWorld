
using System;

namespace GameLib.DI
{
    public interface IDIContext
    {
        static IDIContext New()
        {
            var ctx = new DefaultDIContext();
            ctx.Bind(typeof(DefaultDIContext));
            return ctx;
        }

        IDIContext Bind(Type type);

        IDIContext BindInstance(Type target,object instance);
        IDIContext BindInstance(object instance);
        IDIContext BindInstance(string name, object instance);
        IDIContext BindInstance(Key target,object instance);
        IDIContext BindInstance(string name, Type type, object instance);

        T GetInstance<T>(Type type);
        T GetInstance<T>(string name, Type type);
    }
}