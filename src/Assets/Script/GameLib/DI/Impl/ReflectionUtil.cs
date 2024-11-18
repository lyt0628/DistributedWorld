
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameLib.DI
{
    static class ReflectionUtil
    {

        public static IBinding ResolvePriorBinding(ISet<IBinding> v, Type target)
        {
            if (v.Count == 1) return v.First(); 

            var sortedBindings = v.OrderByDescending(b => b.Priority);
            var first = sortedBindings.First();
            var second = sortedBindings.Skip(1).First();
            if (first.Priority == second.Priority)
            {
                ThrowConflictBindingsDIException(sortedBindings, target);
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

        public static void GenerateInjecionFailException(Type target, string message) {
            throw new DIException("Cannot inject into taret "+ target.FullName+", "+ message);
        }


        public static bool TryGetScopeof(Type type, out ScopeFlag scope)
        {
            if (type.IsDefined(typeof(Scope)))
            {
                if (type.GetCustomAttribute(typeof(Scope), true) is Scope s)
                {
                    scope = s.Value;
                    return true;
                }else
                {
                    throw new DIException("Cannot Get Scope of: " + type.FullName);
                }
            }else
            {
                scope = default;
                return false;
            }
        }

        public static bool TryGetPriorityOf(Type type,out int priority)
        {
             if (type.IsDefined(typeof(Priority)))
            {
                if (type.GetCustomAttribute(typeof(Priority), true) is Priority p)
                {
                    priority = p.Value;
                    return true;

                }else
                {
                    throw new DIException("Cannot Get Scope of: " + type.FullName);
                }
            }else
            {
                priority = default;
                return false;
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

        public static void ThrowConflictBindingsDIException(IOrderedEnumerable<IBinding> sortedBindings, Type target)
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
            ReflectionUtil.GenerateInjecionFailException(
                target,
                "Same Priority Type was Found: " + msg);
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