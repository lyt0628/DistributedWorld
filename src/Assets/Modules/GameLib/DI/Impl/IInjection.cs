using System.Collections.Generic;


namespace GameLib.DI
{
    interface IInjection
    {
        Injector GenInjector(BindingLookup lookup);
        ISet<Key> Dependencies { get; }
    }
}