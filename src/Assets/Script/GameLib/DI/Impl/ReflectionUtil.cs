
using System;

namespace GameLib.DI
{
    static class ReflectionUtil
    {
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
            throw new DIException("Cannot inject into taret"+ target.FullName+", "+ message);
        }
    }

}