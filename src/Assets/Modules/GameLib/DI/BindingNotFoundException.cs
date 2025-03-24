

using System;

namespace GameLib.DI
{
    public class BindingNotFoundException : DIException
    {
        internal BindingNotFoundException(Type type)
            : base($"Cannot find binding for: {type.FullName}.")
        {
        }
        internal BindingNotFoundException(string name, Type type)
    : base($"Cannot find binding for: {type.FullName} with name {name}.")
        {
        }
        internal BindingNotFoundException(string name)
: base($"Cannot find binding with name {name}.")
        {
        }
        internal BindingNotFoundException(Key k)
: base($"Cannot find binding for: {k.Type.FullName} with name {k.Name}.")
        {
        }
    }
}
