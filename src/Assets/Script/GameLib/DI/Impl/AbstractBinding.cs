
using System;
using System.Collections.Generic;

namespace GameLib.DI
{
    abstract class AbstractBinding : IBinding
    {
        private readonly Key target;
        private readonly ISet<Key> dependencies;
        private ScopeFlag scope;

        public AbstractBinding(Key target, ISet<Key> dependencies, ScopeFlag scope = ScopeFlag.Sington) {
            this.target = target;
            this.dependencies = dependencies;
            this.scope = scope;
        }

        public Key Target => target;

        public ISet<Key> Dependencies => dependencies;

        public ScopeFlag Scope {  get { return scope; } set { scope = value; } }

        public bool IsSington => scope == ScopeFlag.Sington;

        public bool IsPrototype => scope == ScopeFlag.Prototype;


        public abstract Builder GenBuilder(BindingLookup lookup);
    }
}
