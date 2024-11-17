
using System;
using System.Collections.Generic;

namespace GameLib.DI
{
    class InstanceBinding : AbstractBinding
    {
        private readonly object instance;
        public InstanceBinding(object instance ,Key target, 
                                ISet<Key> dependencies) 
            : base(target, dependencies, ScopeFlag.Sington)
        {
            this.instance = instance;
        }

        public override Builder GenBuilder(BindingLookup lookup)
        {
            return () => instance;
        }
    }
}
