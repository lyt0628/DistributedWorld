
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;

namespace GameLib.DI
{
    static class Bindings
    {
        public static IBinding ToConstructor(Key target,
                                        Construct contruct,
                                        ISet<Key> dependencies) 
        {
            return new ConstructorBinding( target, contruct, dependencies);
        }

        public static IBinding ToInstance(Key target, object instance)
        {
            return new InstanceBinding(target, instance);
        }
        public static ISet<IBinding> NewEmptyGenericBindingSet()
        {
            return  new HashSet<IBinding>();
        }

        public static ISet<Key> EmptyDeps = new HashSet<Key>();

        public static IInjection CombineInjection<T>(ISet<IInjection> injections)
        {
            return new MultipleInjection(injections);
        }

    }

}