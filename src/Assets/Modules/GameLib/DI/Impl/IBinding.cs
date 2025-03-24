using System.Collections.Generic;

namespace GameLib.DI
{
    interface IBinding
    {
        Key Target { get; }
        ISet<Key> Dependencies { get; }
        ScopeFlag Scope { get; set; }
        int Priority { get; set; }
        bool Lazy { get; set; }

        bool IsSington { get; }
        bool IsPrototype { get; }

        Builder GenBuilder(BindingLookup lookup);
    }
}