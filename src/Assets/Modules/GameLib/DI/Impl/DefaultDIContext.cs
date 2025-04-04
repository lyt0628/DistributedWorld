
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
        public IDIContext Bind<T>(ScopeFlag scope = ScopeFlag.Default)
        {
            var type = typeof(T);
            var target = Key.Get(type);
            Bind0(target, scope);
            return this;
        }

        // <summary> 
        // Bind for type with custom name
        // </summary>
        public IDIContext Bind<T>(string name, ScopeFlag scope = ScopeFlag.Default)
        {
            var type = typeof(T);
            var target = Key.Get(name, type);
            Bind0(target, scope);
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
        public IDIContext BindInstance(string name, Type type, object instance)
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



        // <summary> 
        // Bind sington for type with type.FullName as name
        // </summary>
        public IDIContext BindExternalInstance(Type type, object instance)
        {
            return BindExternalInstance(Key.Get(type), instance);
        }

        // <summary> 
        // Bind sington for Specific Key
        // The Key.Name as name
        // </summary>
        public IDIContext BindExternalInstance(Key target, object instance)
        {
            var binding = Bindings.ToInstance(target, instance);
            AddBinding(binding);
            return this;
        }
        public IDIContext BindExternalInstance(string name, Type type, object instance)
        {
            var target = Key.Get(name, type);
            return BindExternalInstance(target, instance);
        }

        public IDIContext BindExternalInstance(object instance)
        {
            return BindExternalInstance(instance.GetType(), instance);
        }


        public IDIContext BindExternalInstance(string name, object instance)
        {
            return BindExternalInstance(name, instance.GetType(), instance);
        }

        /// <summary>
        /// Get instance By type and name. 
        /// If multiple instances match the type and name, resolve those by priority.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public T GetInstance<T>(string name)
        {
            var type = typeof(T);
            return (T)GetInstance(name, type);
        }


        /// <summary>
        /// Get instance by type with default name
        /// If multiple instances match the type and name, resolve those by priority.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public T GetInstance<T>()
        {
            var type = typeof(T);
            return (T)GetInstance(type);
        }
        public object GetInstance(Type type)
        {
            if (!type.IsInterface)
            {
                Debug.LogWarning($"{type} is a concrete class");
            }
            var key = Key.Get(type);
            return GetInstance(key);
        }

        public object GetInstance(string name, Type type)
        {
            //if (!type.IsInterface)
            //{
            //    Debug.LogWarning($"{type} is a concrete class");
            //}

            var key = Key.Get(name, type);

            return GetInstance(key);
        }

        object GetInstance(Key key)
        {
            var type = key.Type;
            //if (!type.IsInterface)
            //{
            //    Debug.LogWarning($"{type} is a concrete class");
            //}
            var binding = LookupBindingWithParent(key);

            return GenInstance(binding);
        }

        public IDIContext Inject(object target)
        {
            var k = Key.Get(target.GetType());
            var binding = Bindings.ToInstance(k, target);
            binding = BindingWithInjection(k, binding);
            binding.Scope = ScopeFlag.Prototype; // All Directly Inject into a object see as  prototype, which was created by client
            var _ = GenInstance(binding);
            return this;
        }


        public IDIContext GetParent()
        {
            return parentCtx;
        }

        public IDIContext SetParent(IDIContext dIContext)
        {
            parentCtx = dIContext;
            return this;
        }
        #endregion


        readonly Dictionary<Type, ISet<IBinding>> bindings = new();

        readonly IDictionary<Key, InstanceBinding> scopeCahce
            = new Dictionary<Key, InstanceBinding>();

        readonly IDictionary<Key, IBinding> depCache = new Dictionary<Key, IBinding>();
        IDIContext parentCtx = new EmptyDIContext();

        /// <summary>
        /// Bind step1: Disgush interface, abstract class, and concrete class
        /// </summary>
        /// <param name="target"></param>
        /// <param name="scope"></param>
        void Bind0(Key target, ScopeFlag scope)
        {
            var type = target.Type;
            if (DIUtil.IsInterface(type))
            {
                if (!bindings.ContainsKey(target.Type))
                {
                    bindings.Add(target.Type, Bindings.NewEmptyGenericBindingSet());
                }
            }
            else if (!DIUtil.IsAbstract(type))
            {
                DoBind(target, scope);
            }
        }

        /// <summary>
        /// Bind step2: Generate constructor binding for source class
        /// Bind step3: lookup Binding of dependencies
        /// </summary>
        /// <param name="target"></param>
        /// <param name="scope"></param>
        void DoBind(Key target, ScopeFlag scope)
        {
            // A Constructor should not be bound twice. with the same Key
            // For Reenterable, How to check a repeat Construcor???
            // A Key  is Unique ID for A bindings
            if (bindings.TryGetValue(target.Type, out ISet<IBinding> bindingSet))
            {
                if (bindingSet.Where(b => b.Target == target).Any())
                {
                    return;
                }
            }

            IBinding binding = GenerateConstuctorBinding(target);
            if (scope != ScopeFlag.Default)
            {
                binding.Scope = scope;
            }

            binding = BindingWithInjection(target, binding);
            //if (!binding.Lazy)
            //{
            //    Debug.Log("Not Lazy");
            //}
            if (!binding.Lazy && binding.IsSington)
            {
                var i = GenInstance(binding);
                binding = Bindings.ToInstance(binding.Target, i);
            }
            AddBinding(binding);

        }


        /// <summary>
        /// Bind step4: add Binding to pool bindins
        /// </summary>
        /// <param name="binding"></param>
        void AddBinding(IBinding binding)
        {
            DoAddBinding(binding.Target, binding);

            var ancestors = DIUtil.CollectAncestorsAndInterfaces(binding.Target.Type);
            foreach (var ancestor in ancestors)
            {
                DoAddBinding(Key.Get(binding.Target.Name, ancestor), binding);
            }
        }

        void DoAddBinding(Key target, IBinding binding)
        {
            if (bindings.TryGetValue(target.Type, out ISet<IBinding> mBindings))
            {
                mBindings.Add(binding);
            }
            else
            {
                var bindSet = Bindings.NewEmptyGenericBindingSet();
                bindSet.Add(binding);
                bindings.Add(target.Type, bindSet);
            }
        }


        IBinding BindingWithInjection(Key target, IBinding binding)
        {
            Type type = target.Type;

            if (DIUtil.TryGetScopeof(type, out ScopeFlag s, out bool l))
            {
                binding.Scope = s;
                binding.Lazy = l;
            }

            if (DIUtil.TryGetPriorityOf(type, out int prioriy))
            {
                binding.Priority = prioriy;
            }

            DIUtil.CollectAllFieldAndPropsInHierachy(type,
                out List<FieldInfo> fieldInfos,
                out List<PropertyInfo> propInfos);

            DIUtil.CollectInjectionFromFieldsAndProps(
                fieldInfos, propInfos, out ISet<IInjection> injections);

            if (injections.Count == 1)
            {
                binding = new InjectedBinding(binding, injections.First(), binding.Lazy, depCache);
            }
            if (injections.Count > 1)
            {
                var combinedInjecion = new MultipleInjection(injections);
                binding = new InjectedBinding(binding, combinedInjecion, binding.Lazy, depCache);
            }
            return binding;
        }


        IBinding GenerateConstuctorBinding(Key target)
        {
            Type type = target.Type;
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
                binding = DIUtil.BindingFromInjectedConstructor(target, injectedCtorInfos.First());
            }
            else
            {
                binding = DIUtil.BindingFromEmptyConstructor(target);
            }

            return binding;
        }

        object GenInstance(IBinding binding)
        {
            var builder = binding
                             .GenBuilder((k) =>
                             {
                                 return LookupBinding(k);
                             });
            if (builder != null)
            {
                var instance = builder();
                return instance;
            }
            throw new DIException("You Bind a null as a InstanceBinding for :" + binding.Target.Name);
        }


        /**
        * Lookup Binding for k.
        */
        IBinding LookupBinding(Key k)
        {

            if (depCache.TryGetValue(k, out var binding))
            {
                return binding;
            }

            //return DoLookupBinding(k);
            return LookupBindingWithParent(k);
        }

        IBinding LookupBindingWithParent(Key k)
        {
            IBinding binding;
            try
            {
                binding = DoLookupBinding(k);
            }
            catch (BindingNotFoundException)
            {
                var instance = parentCtx.GetInstance(k.Name, k.Type);
                binding = Bindings.ToInstance(k, instance);
            }

            return binding;
        }
        IBinding DoLookupBindingByName(string name)
        {
            // Filter By Name
            var namedBindings = DIUtil.ResolveNamedBinding(name, bindings.SelectMany(dict => dict.Value).ToHashSet());
            if (namedBindings.Count == 1)
            {
                var binding = namedBindings.First();
                return ScopeBinding(binding.Target, binding);
            }
            else if (namedBindings.Count == 0)
            {
                throw new BindingNotFoundException(name);
            }

            // Filter By Priority
            IBinding priorBinding = DIUtil.ResolvePriorBinding(namedBindings);
            return ScopeBinding(priorBinding.Target, priorBinding);
        }

        IBinding DoLookupBinding(Key k)
        {
            if (k.Type == null && !string.IsNullOrEmpty(k.Name))
            {
                return DoLookupBindingByName(k.Name);
            }
            return DoLookupBindingByTypeAndName(k);

        }

        private IBinding DoLookupBindingByTypeAndName(Key k)
        {
            // Filter By Type
            if (!bindings.TryGetValue(k.Type, out ISet<IBinding> typedBindings))
            {
                throw new BindingNotFoundException(k.Type);
            }

            if (typedBindings == null || typedBindings.Count == 0)
            {
                throw new BindingNotFoundException(k.Type);
            }

            if (typedBindings.Count == 1)
            {
                // If only one coordiante rest, return it.
                // Not check Name
                return ScopeBinding(k, typedBindings.First());
            }

            // Else User should Specific Name for detailed Binding.

            // Filter By Name
            var namedBindings = DIUtil.ResolveNamedBinding(k.Name, typedBindings);
            if (namedBindings.Count == 1) return ScopeBinding(k, namedBindings.First());
            else if (namedBindings.Count == 0)
            {
                throw new BindingNotFoundException(k);
            }

            // Filter By Priority
            IBinding priorBinding = DIUtil.ResolvePriorBinding(namedBindings);
            return ScopeBinding(k, priorBinding);
        }

        /// 调用它的时候，这个绑定就是最终的绑定了，只是想知道这个绑定是否产生过实例而已
        IBinding ScopeBinding(Key k, IBinding binding)
        {
            IBinding resolvedBinding = binding;
            if (binding.Scope == ScopeFlag.Sington)
            {
                // 類型和名稱共同構成 一個綁定的Key
                // 默認綁定的名稱和實例類的名稱一樣，
                // 只綁定一個實例的時候，不會查詢名稱，所以根據接口查找可以找到
                // 但是這邊做緩存的時候，是根據完整的key來做的
                // 我得爲父類一起緩存才行，不然緩存在不同層級會失效
                // 唯独在只有一个绑定的情况下，这里不需要考虑名字
                if (scopeCahce.TryGetValue(binding.Target, out InstanceBinding b))
                {
                    resolvedBinding = b;
                }
                else if (binding is InstanceBinding i)
                {
                    DoScopeBinding(i);
                }
                else
                {
                    var instance = GenInstance(binding);
                    var instanceBinding = new InstanceBinding(binding.Target, instance);
                    resolvedBinding = instanceBinding;
                    DoScopeBinding(instanceBinding);
                }
            }

            return resolvedBinding;
        }

        private void DoScopeBinding(InstanceBinding i)
        {
            scopeCahce.Add(i.Target, i);
        }

        public bool TryGetInstance<T>(string name, out T value)
        {
            try
            {
                value = GetInstance<T>(name);
            }
            catch (BindingNotFoundException)
            {
                value = default;
                return false;
            }
            return true;
        }

        public object GetInstance(string name)
        {
            return GetInstance(new Key(name, null));
        }
    }
}