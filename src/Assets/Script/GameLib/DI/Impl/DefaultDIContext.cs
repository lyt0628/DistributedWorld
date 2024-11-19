
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GameLib.DI
{
    class DefaultDIContext : IDIContext
    {

        #region [[ API ]]

        /// <summary>
        /// Bind for type, using The Type.FullName as name
        /// </summary>
        public IDIContext Bind(Type type)
        {
            var target = Key.Get(type);
            Bind0(target);
            return this;
        }

        // <summary> 
        // Bind for type with custom name
        // </summary>
        public IDIContext Bind(string name, Type type)
        {
            var target = Key.Get(name, type);
            Bind0(target);
            return this;
        }

        // <summary> 
        // Bind sington for type with type.FullName as name
        // </summary>
        public IDIContext BindInstance(Type type, object instance)
        {
            return BindInstance(Key.Get(type), instance);
        }

        // <summary> 
        // Bind sington for Specific Key
        // The Key.Name as name
        // </summary>
        public IDIContext BindInstance(Key target, object instance)
        {
            var binding = Bindings.ToInstance(target, instance);
            var mBinding = BindingWithInjection(target, binding);
            AddBinding(mBinding);
            return this;
        }
        public IDIContext BindInstance(string name, Type type,object instance)
        {
            var target = Key.Get(name, type);
            return BindInstance(target, instance);
        }

        public IDIContext BindInstance(object instance)
        {
            return BindInstance(instance.GetType(), instance);
        }

        public IDIContext BindInstance(string name, object instance)
        {
            return BindInstance(name, instance.GetType(), instance);
        }

        /// <summary>
        /// Get instance By type and name. 
        /// If multiple instances match the type and name, resolve those by priority.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public T GetInstance<T>(string name, Type type)
        {
            // Filter By Type
            ISet<IBinding> typedBindings = default;
            try
            {
                typedBindings = bindings.Where(
                 pair => pair.Key.Type == type
                 ).First().Value;
            }
            catch (Exception)
            {
                ReflectionUtil.ThrowNoBindingFoundException(type);
            }
            if(typedBindings.Count == 0)
            {
                ReflectionUtil.ThrowNoBindingFoundException(type);
            }

            // Filter By Name
            var namedBinding = typedBindings
                .Where(b => b.Target.Name == name)
                .ToHashSet();
            if(namedBinding.Count == 0)
            {
                ReflectionUtil.ThrowNoBindingFoundException(name, type);
            }

            var binding = ReflectionUtil.ResolvePriorBinding(namedBinding, type);

            return GenInstance<T>(Key.Get(name, type), binding);
        }


        /// <summary>
        /// Get instance by type, 
        /// if multiple instances match the Type, resolve those by Priority.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public T GetInstance<T>(Type type)
        { 
            // Filter By Type
            ISet<IBinding> myBindings = default;
            try
            {
                myBindings = bindings.Where(
                 pair => pair.Key.Type == type
                 ).First().Value;
            }
            catch (Exception)
            {
                ReflectionUtil.ThrowNoBindingFoundException(type);
            }
             if(myBindings.Count == 0)
            {
                ReflectionUtil.ThrowNoBindingFoundException(type);
            }
           var binding = ReflectionUtil.ResolvePriorBinding(myBindings, type);
            return GenInstance<T>(Key.Get(type), binding);
        }


        public IDIContext Inject(object target)
        {
            var k = Key.Get(target.GetType());
            var binding = Bindings.ToInstance(k, target);
            binding = BindingWithInjection(k, binding);
            GenInstance<object>(k, binding);
            return this;
        }
        #endregion


        private readonly IDictionary<TypeKey, ISet<IBinding>> bindings 
                = new Dictionary<TypeKey, ISet<IBinding>>() { };

        private readonly IDictionary<Key, InstanceBinding> scopeCahce 
            = new Dictionary<Key,InstanceBinding>();

        private void Bind0(Key target)
        {
            var type = target.Type;
            if (ReflectionUtil.IsInterface(type))
            {
                if (!bindings.ContainsKey(target.KeyType))
                {
                    bindings.Add(target.KeyType, Bindings.NewEmptyGenericBindingSet());
                }
            }
            else if (!ReflectionUtil.IsAbstract(type))
            {
                DoBind(target);
            }
        }

        //private readonly ISet<Key> depChain = new HashSet<Key>();
        private void DoBind(Key target)
        {
            // A Constructor should not be bound twice. with the same Key
            // For Reenterable, How to check a repeat Construcor???
            // A Key  is Unique ID for A bindings
            if (bindings.TryGetValue(target.KeyType, out ISet<IBinding> bindingSet))
            {
                if (bindingSet.Where(b=>b.Target == target).Any())
                {
                    return;
                }
            }

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
            if (bindings.TryGetValue(target.KeyType, out ISet<IBinding> mBindings))
            {
                mBindings.Add(binding);
            }
            else
            {
                var bindSet = Bindings.NewEmptyGenericBindingSet();
                bindSet.Add(binding);
                bindings.Add(target.KeyType, bindSet);
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

        readonly IDictionary<Key,IBinding> depCache = new Dictionary<Key,IBinding>();
        private IBinding BindingWithInjection(Key target,IBinding binding)
        {
            Type type = target.Type;

            if (ReflectionUtil.TryGetScopeof(type, out ScopeFlag s))
            {
                binding.Scope = s;
            }
            if(ReflectionUtil.TryGetPriorityOf(type, out int prioriy))
            {
                binding.Priority = prioriy;
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
                    var k = ReflectionUtil.GenerateKeyForInjectedProperty(propInfo);

                    var injection = new SetterInjection((instance, value) =>
                    {
                        propInfo.SetValue(instance, value);
                    },k);
                    injections.Add(injection);
                }
            }

            foreach (var fieldInfo in fieldInfos)
            {
                if (fieldInfo.IsDefined(typeof(Injected), true))
                {
                    var k = ReflectionUtil.GenerateKeyForInjectedField(fieldInfo);
                    var injection = new SetterInjection((instance, value) =>
                    {

                        fieldInfo.SetValue(instance, value);
                    }, k);
                    injections.Add(injection);
                }
            }

            if (injections.Count == 1) {
                binding = new InjectedBinding(binding, injections.First(), depCache);
            }
            if (injections.Count > 1)
            {
                var combinedInjecion = new MultipleInjection(injections);
                binding = new InjectedBinding(binding, combinedInjecion, depCache);
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

            var emptyCtor = type.GetConstructor(Type.EmptyTypes) ?? throw new DIException("No Empty Constructor Found for: " + type.FullName);
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
        private T GenInstance<T>(Key target, IBinding binding)
        {
            var resolvedBinding = binding;
            object instance = default;
            if (binding.IsSington)
            {
                if (scopeCahce.TryGetValue(target, out InstanceBinding scopedBinding)) {
                    resolvedBinding = scopedBinding;
                }
                else if (binding is InstanceBinding instanceBinding) {
                    {
                        scopeCahce.Add(target, instanceBinding);
                    }
                }
                else
                {
                    instance = DoGenInstance(target, resolvedBinding);
                    scopeCahce.Add(target, new InstanceBinding(target, instance));
                }
            }
            if (null == instance)
            {
                instance =  DoGenInstance(target, resolvedBinding);
            }
            return (T)instance;
        }

        private object DoGenInstance(Key target, IBinding binding)
        {
                var builder = binding
                                 .GenBuilder((k) =>
                                 {
                                     return LookupBinding(k, target.Type);
                                 });
                if (builder != null)
                {
                    var instance = builder();
                    return instance;
                }
                throw new DIException("Invalid Constructor or instance was bound to:" + target.Type.FullName);
        }



        /**
        * Lookup Binding for k.
        */
        private IBinding LookupBinding(Key k, Type target)
        {

            if (depCache.TryGetValue(k, out var binding))
            {
                return binding;
            }

            // Filter By Type
            if (!bindings.TryGetValue(k.KeyType, out ISet<IBinding> typedBindings))
            {
                ReflectionUtil.GenerateInjecionFailException(
                target,
                "Dependency cannot be found : " + k.Type.FullName +
                "Are you Bind The Class to DIContext?");
            }

            if (typedBindings == null || typedBindings.Count == 0)
            {
                ReflectionUtil.GenerateInjecionFailException(
                    target,
                    "No Bindings Founded for: " + k.Type.FullName);
            }

            if (typedBindings.Count == 1)
            {
                    // If only one coordiante rest, return it.
                    // Not check Name
                    return ScopeBinding(k, typedBindings.First());
            }

                // Else User should Specific Name for detailed Binding.

                // Filter By Name
            var namedBindings = ResolveNamedBinding(k.Name, typedBindings, target);
            if (namedBindings.Count == 1) return ScopeBinding(k, namedBindings.First());
            else if (namedBindings.Count == 0)
            {
                    Debug.LogWarning("No Binding found for: " + k.Type.FullName + " with name: "
                                      + k.Name + ", IDIContext will still resolve By Priority.\n"
                                      + "But, IDIContext suggest you to resolve this situation by specific proper Name in Injected Attribute");
            }
            IBinding priorBinding;
            // Filter By Priority
            if (namedBindings.Count == 0)
            {
                    priorBinding = ReflectionUtil.ResolvePriorBinding(typedBindings, target);
            }
            else
            {
                    priorBinding = ReflectionUtil.ResolvePriorBinding(namedBindings, target);
            }

            var scopedBindning = ScopeBinding(k, priorBinding);

            return scopedBindning;
            

        }

        private IBinding ScopeBinding(Key k, IBinding binding)
        {
            IBinding resolvedBinding = binding;
            if (binding.Scope == ScopeFlag.Sington)
            {
                if (scopeCahce.TryGetValue(k, out InstanceBinding b))
                {
                    resolvedBinding = b;
                }
                else if(binding is InstanceBinding i)
                {
                    scopeCahce.Add(k, i);
                }
                else
                {
                    var instance = DoGenInstance(k, binding);
                    var instanceBinding = new InstanceBinding(k, instance);
                    resolvedBinding = instanceBinding;
                    scopeCahce.Add(k, instanceBinding);
                }
            }

            return resolvedBinding;
        }

        private static ISet<IBinding> ResolveNamedBinding(string name, ISet<IBinding> v, Type target)
        {
            var namedBindings = v.Where(b => name == b.Target.Name).ToHashSet();

            return namedBindings;
        }


    }
}