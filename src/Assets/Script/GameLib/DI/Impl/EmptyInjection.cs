


using System.Collections.Generic;

namespace GameLib.DI
{
    class EmptyInjection : Sington<EmptyInjection>, IInjection
    {
        public ISet<Key> Dependencies => Bindings.EmptyDeps;

        public Injector GenInjector(BindingLookup lookup)
        {
            return (_) => { };
        }
    }
}