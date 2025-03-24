
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameLib.DI
{
    internal static class DIUtil
    {

        public static IBinding ResolvePriorBinding(ISet<IBinding> v)
        {
            if (v.Count == 1) return v.First();

            var sortedBindings = v.OrderByDescending(b => b.Priority);
            var first = sortedBindings.First();
            var second = sortedBindings.Skip(1).First();
            if (first.Priority == second.Priority)
            {
                ThrowConflictBindingsDIException(sortedBindings);
            }
            return first;
        }


        public static bool IsInterface(Type type)
        {
            return type.IsInterface;
        }
        public static bool IsAbstract(Type type)
        {
            return type.IsAbstract;
        }

        public static string GenerateNameOf(Type type)
        {
            return type.Name;
        }

        public static void GenerateInjecionFailException(Type target, string message)
        {
            throw new DIException("Cannot inject into taret " + target.FullName + ", " + message);
        }


        public static bool TryGetScopeof(Type type, out ScopeFlag scope, out bool lazy)
        {
            if (type.IsDefined(typeof(Scope)))
            {
                if (type.GetCustomAttribute(typeof(Scope), true) is Scope s)
                {
                    scope = s.Value;
                    lazy = s.Lazy;
                    //if (!lazy)
                    //{
                    //    Debug.Log("Real Not Lazy");
                    //}
                    return true;
                }
                else
                {
                    throw new DIException("Cannot Get Scope of: " + type.FullName);
                }
            }
            else
            {
                scope = ScopeFlag.Sington;
                lazy = true;
                return false;
            }
        }

        public static bool TryGetPriorityOf(Type type, out int priority)
        {
            if (type.IsDefined(typeof(Priority)))
            {
                if (type.GetCustomAttribute(typeof(Priority), true) is Priority p)
                {
                    priority = p.Value;
                    return true;

                }
                else
                {
                    throw new DIException("Cannot Get Scope of: " + type.FullName);
                }
            }
            else
            {
                priority = default;
                return false;
            }
        }
        public static void CollectAllFieldAndPropsInHierachy(Type type, out List<FieldInfo> fieldInfos, out List<PropertyInfo> propInfos)
        {
            fieldInfos = new List<FieldInfo>();
            propInfos = new List<PropertyInfo>();
            var access = BindingFlags.Public | BindingFlags.NonPublic |
                         BindingFlags.Instance | BindingFlags.FlattenHierarchy;

            Type currentType = type;
            while (currentType != null && currentType.GetType() != typeof(object))
            {
                fieldInfos.AddRange(currentType.GetFields(access));
                propInfos.AddRange(currentType.GetProperties(access));
                currentType = currentType.BaseType;
            }
        }

        public static Key GenerateKeyForInjectedProperty(PropertyInfo propInfo)
        {
            var inj = propInfo.GetCustomAttribute(typeof(Injected)) as Injected;
            Key k;
            if (inj.Name != null)
            {
                k = Key.Get(inj.Name, propInfo.PropertyType);
            }
            else
            {
                k = Key.Get(propInfo.PropertyType);
            }
            return k;
        }


        public static Key GenerateKeyForInjectedField(FieldInfo propInfo)
        {
            var inj = propInfo.GetCustomAttribute(typeof(Injected)) as Injected;
            Key k;
            if (inj.Name != null)
            {
                k = Key.Get(inj.Name, propInfo.FieldType);
            }
            else
            {
                k = Key.Get(propInfo.FieldType);
            }
            return k;
        }

        public static void CollectInjectionFromFieldsAndProps(List<FieldInfo> fieldInfos, List<PropertyInfo> propInfos, out ISet<IInjection> injections)
        {
            injections = new HashSet<IInjection>();
            foreach (var injection in from propInfo in propInfos
                                      where propInfo.IsDefined(typeof(Injected), true)
                                      let k = DIUtil.GenerateKeyForInjectedProperty(propInfo)
                                      let injection = new SetterInjection((instance, value) =>
                                      {
                                          propInfo.SetValue(instance, value);
                                      }, k)
                                      select injection)
            {
                injections.Add(injection);
            }

            foreach (var injection in from fieldInfo in fieldInfos
                                      where fieldInfo.IsDefined(typeof(Injected), true)
                                      let k = DIUtil.GenerateKeyForInjectedField(fieldInfo)
                                      let injection = new SetterInjection((instance, value) =>
                                      {

                                          fieldInfo.SetValue(instance, value);
                                      }, k)
                                      select injection)
            {
                injections.Add(injection);
            }
        }

        public static IBinding BindingFromInjectedConstructor(Key target, ConstructorInfo injectedCtor)
        {
            IBinding binding;
            // struct 类型不支持有参构造注入
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

        public static IBinding BindingFromEmptyConstructor(Key target)
        {
            Type type = target.Type;
            IBinding binding;
            // struct 的情况 一定有默认构造
            if (type.IsValueType && type.IsSealed && !type.IsPrimitive)
            {
                binding = Bindings.ToConstructor(target,
                    // 闭包绑定
                    (object[] args) => Activator.CreateInstance(type),
                    Bindings.EmptyDeps);
            }
            else
            {
                var emptyCtor = type.GetConstructor(Type.EmptyTypes) ?? throw new DIException("No Empty Constructor Found for: " + type.FullName);
                binding = Bindings.ToConstructor(target,
                                    (object[] args) => emptyCtor.Invoke(args),
                                    Bindings.EmptyDeps);
            }
            return binding;
        }

        public static ISet<IBinding> ResolveNamedBinding(string name, ISet<IBinding> v)
        {
            var namedBindings = v.Where(b => name == b.Target.Name).ToHashSet();

            return namedBindings;
        }


        public static ISet<Type> CollectAncestorsAndInterfaces(Type type)
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

        public static void ThrowNoBindingFoundException(Type target)
        {
            throw new DIException(
                    "No Bind Found for " + target.FullName);
        }

        public static void ThrowNoBindingFoundException(string name, Type type)
        {
            throw new DIException(
                "No Bind Found for " + type.FullName + "with name: " + name);
        }

        public static void ThrowMoreThanOneBindingFoundFor(string name, Type type)
        {
            throw new DIException(
                "More than one Binding Found for " + type.FullName + "with name: " + name);
        }

        public static void ThrowConflictBindingsDIException(IOrderedEnumerable<IBinding> sortedBindings)
        {
            var conflictPriority = sortedBindings.First().Priority;
            ISet<Type> conflictTypes = new HashSet<Type>();
            foreach (var binding in sortedBindings)
            {
                if (binding.Priority == conflictPriority)
                {
                    conflictTypes.Add(binding.Target.Type);
                }
            }
            var msg = "[ ";
            foreach (var t in conflictTypes)
            {
                msg += t.FullName + ", ";
            }
            msg += " ]";
            throw new DIException($"Conflict Binding was found {msg}");
        }

        public static void ThrowNoEmptyConstructorException(Type type)
        {
            throw new DIException("No Empty Constructor Found for: " + type.FullName);
        }

        public static void ThrowCircurDependenciesException(Type type)
        {
            throw new DIException("Circur Depdencies is not allowed in :" + type.FullName);
        }
    }

}