
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;

namespace GameLib.DI
{
    static class Bindings
    {
        public static AbstractBinding ToConstructor(Key target,
                                        Construct contruct,
                                        ISet<Key> dependencies) 
        {
            return new ConstructorBinding( target, contruct, dependencies);
        }

        public static ISet<IBinding> NewEmptyGenericBindingSet()
        {
            return  new HashSet<IBinding>();
        }

        public static ISet<Key> EmptyDeps = ImmutableHashSet<Key>.Empty();

        public static IInjection CombineInjection<T>(ISet<IInjection> injections)
        {
            return new MultipleInjection(injections);
        }

    }

}