
using System;

namespace GameLib.DI
{
    class EmptyDIContext : IDIContext
    {
        public IDIContext Bind<T>(ScopeFlag scope = ScopeFlag.Default)
        {
            throw new InvalidOperationException();
        }

        public IDIContext Bind<T>(string name, ScopeFlag scope = ScopeFlag.Default)
        {
            throw new InvalidOperationException();
        }

        public IDIContext BindExternalInstance(Type target, object instance)
        {
            throw new NotImplementedException();
        }

        public IDIContext BindExternalInstance(object instance)
        {
            throw new NotImplementedException();
        }

        public IDIContext BindExternalInstance(string name, object instance)
        {
            throw new NotImplementedException();
        }

        public IDIContext BindExternalInstance(Key target, object instance)
        {
            throw new NotImplementedException();
        }

        public IDIContext BindExternalInstance(string name, Type type, object instance)
        {
            throw new NotImplementedException();
        }

        public IDIContext BindInstance(Type target, object instance)
        {
            throw new InvalidOperationException();
        }

        public IDIContext BindInstance(object instance)
        {
            throw new InvalidOperationException();
        }

        public IDIContext BindInstance(string name, object instance)
        {
            throw new InvalidOperationException();
        }

        public IDIContext BindInstance(Key target, object instance)
        {
            throw new InvalidOperationException();
        }

        public IDIContext BindInstance(string name, Type type, object instance)
        {
            throw new InvalidOperationException();
        }

        public T GetInstance<T>()
        {
            throw BindingNotFoundException.Of(typeof(T));
        }

        public T GetInstance<T>(string name)
        {
            throw BindingNotFoundException.Of(typeof(T));
        }

        public object GetInstance(Type type)
        {
            throw BindingNotFoundException.Of(type);
        }

        public object GetInstance(string name, Type type)
        {
            throw BindingNotFoundException.Of(type);
        }

        public IDIContext GetParent()
        {
            throw new InvalidOperationException();
        }

        public IDIContext Inject(object target)
        {
            throw BindingNotFoundException.Of(target.GetType());
        }

        public IDIContext SetParent(IDIContext dIContext)
        {
            throw new InvalidOperationException();
        }
    }

}
