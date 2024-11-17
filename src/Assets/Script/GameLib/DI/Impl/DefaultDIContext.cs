
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameLib.DI
{
    class DefaultDIContext : IDIContext
    {
        private readonly IDictionary<Key, ISet<IBinding>> bindings 
                = new Dictionary<Key, ISet<IBinding>>() { };

        public IDIContext Bind(Type type)
        {
            var target = Key.Get(type);

            if (ReflectionUtil.IsInterface(type))
            {
                if (!bindings.ContainsKey(target))
                {
                    bindings.Add(target, Bindings.NewEmptyGenericBindingSet());
                }
            }else if(!ReflectionUtil.IsAbstract(type))
            {
                DoBind(target);
            }
            return this;
        }

        private void DoBind(Key target)
        {
            IBinding binding = GenerateConstuctorBinding(target);

            binding = BindingWithInjection(target, binding);
            AddBinding(binding);
        }

        private void AddBinding(IBinding binding)
        {
            DoAddBinding(binding.Target, binding);

            var ancestors = CollectAncestorsAndInterfaces(binding.Target.Type);
            foreach (var ancestor in ancestors) {
                DoAddBinding(Key.Get(ancestor), binding);
            }
        }

        private void DoAddBinding(Key target, IBinding binding)
        {
            if (bindings.TryGetValue(target, out ISet<IBinding> mBindings))
            {
                mBindings.Add(binding);
            }
            else
            {
                var bindSet = Bindings.NewEmptyGenericBindingSet();
                bindSet.Add(binding);
                bindings.Add(target, bindSet);
            }
        }

        private ISet<Type> CollectAncestorsAndInterfaces(Type type)
        {
            Type[] interfaces = type.GetInterfaces();
            ISet<Type> ancestors = new HashSet<Type>();
            Type currentType = type.BaseType;
            while (currentType != null && currentType != typeof(object))
            {
                ancestors.Add(currentType);
                currentType = currentType.BaseType;
            }
            return ancestors.Union(interfaces).ToHashSet();
        }

        private IBinding BindingWithInjection(Key target,IBinding binding)
        {
            Type type = target.Type;

            if (type.IsDefined(typeof(Scope)))
            {
                if (type.GetCustomAttribute(typeof(Scope), true) is Scope scope)
                {
                    binding.Scope = scope.Value;
                }
            }
            
            var access = BindingFlags.Public | BindingFlags.NonPublic |
                         BindingFlags.Instance;
            var fieldInfos = type.GetFields(access);
            var propInfos = type.GetProperties(access);
            ISet<IInjection> injections = new HashSet<IInjection>();


            foreach (var propInfo in propInfos)
            {
                if (propInfo.IsDefined(typeof(Injected), true))
                {
                    var injection = new SetterInjection((instance, value) =>
                    {
                        propInfo.SetValue(instance, value);
                    },
                        Key.Get(propInfo.PropertyType));
                    injections.Add(injection);
                }
            }

            foreach (var fieldInfo in fieldInfos)
            {
                if (fieldInfo.IsDefined(typeof(Injected), true))
                {
                    var injection = new SetterInjection((instance, value) =>
                    {

                        fieldInfo.SetValue(instance, value);
                    },
                        Key.Get(fieldInfo.FieldType));
                    injections.Add(injection);
                }
            }

            if (injections.Count == 1) {
                binding = new InjectedBinding(binding, injections.First());
            }
            if (injections.Count > 1)
            {
                var combinedInjecion = new MultipleInjection(injections);
                binding = new InjectedBinding(binding, combinedInjecion);
            }
            return binding;
        }

        private IBinding GenerateConstuctorBinding(Key target)
        {
            Type type= target.Type;
            IBinding binding;

            var injectedCtorInfos = type.GetConstructors()
                            .ToList()
                            .Where(ctor => ctor.IsDefined(typeof(Injected), false));

            if (injectedCtorInfos.Any())
            {
                if (injectedCtorInfos.Count() > 1)
                {
                    throw new DIException("More than one Contructor Defined by @" + typeof(Injected).Name);
                }
                binding = BindingFromInjectedConstructor(target, injectedCtorInfos.First());
            }
            else
            {
                binding = BindingFromEmptyConstructor(target);
            }

            return binding;
        }

        private static IBinding BindingFromEmptyConstructor(Key target)
        {
            Type type = target.Type;
            IBinding binding;
            var emptyCtor = type.GetConstructor(Type.EmptyTypes);
            if (emptyCtor == null)
            {
                throw new DIException("No Empty Constructor Found for: " + type.FullName);
            }
            binding = Bindings.ToConstructor(target,
                                (object[] args) => emptyCtor.Invoke(args),
                                Bindings.EmptyDeps);
            return binding;
        }

        private static IBinding BindingFromInjectedConstructor(Key target, ConstructorInfo injectedCtor)
        {
            IBinding binding;

            var deps = injectedCtor.GetParameters()
                    .ToList()
                    .Select(param => param.ParameterType)
                    .Select(t => Key.Get(t))
                    .ToHashSet();

            binding = Bindings.ToConstructor(target,
                (object[] args) => injectedCtor.Invoke(args),
                deps);
            return binding;
        }

        public T GetInstance<T>(Type type)
        {
           var builder  = bindings
                            .Where(key => key.Key.Type == type)
                            .First().Value
                            .First()
                            .GenBuilder((k)=>
                            {
                                return LookupBinding(type, k);
                            });
            if (builder != null)
            {
                return (T)builder();
            }

            return default;
        }

        /**
         * Lookup at bindings, using k to find binding
         */
        private IBinding LookupBinding(Type target, Key k)
        {
            ISet<IBinding> v = default;
            if (bindings.ContainsKey(k))
            {
                v = bindings[k];
            } 
            else
            {
                ReflectionUtil.GenerateInjecionFailException(
                target, "Dependency cannot be found : " + k.Type.FullName);
            }

            if (v == null || v.Count == 0) {
                ReflectionUtil.GenerateInjecionFailException(
                    target, "No Bindings Founded for: " + k.Type.FullName);
            }
            return v.First();
        }
    }
}