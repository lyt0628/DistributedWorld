

using System;

namespace GameLib.DI
{
    class BindingNotFoundException : Exception
    {
        public BindingNotFoundException(Type type)
            : base($"Cannot find binding for: {type.FullName}.")
        {
        }
        public static BindingNotFoundException Of(Type type)
        {
            return new BindingNotFoundException(type);
        }
    }
}
