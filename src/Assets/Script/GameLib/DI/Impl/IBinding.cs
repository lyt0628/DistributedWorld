
using System;
using System.Collections.Generic;

namespace GameLib.DI
{
    interface IBinding
    {
        public Key Target { get; }
        public ISet<Key> Dependencies { get; }
        public ScopeFlag Scope { get; set; }
        public int Priority { get; set; }

        public bool IsSington { get; }
        public bool IsPrototype { get; }

        public Builder GenBuilder(BindingLookup lookup);
    }
}