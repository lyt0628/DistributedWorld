

using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLib.DI
{
    class InjectedBinding : AbstractBinding
    {
        private readonly IInjection injection;
        private readonly IBinding rawBinding;
        private readonly IDictionary<Key, IBinding> depChain;
        public InjectedBinding(IBinding rawBinding,
                               IInjection injection,
                               IDictionary<Key, IBinding> depChain) 
            : base(rawBinding.Target, 
                  rawBinding.Dependencies
                  .Union(injection.Dependencies)
                  .ToHashSet(), 
                  rawBinding.Scope)
        {
            this.injection = injection;
            this.rawBinding = rawBinding;
            this.depChain = depChain;
        }

        public override Builder GenBuilder(BindingLookup lookup)
        {
            return () =>
            {
                var instance = rawBinding.GenBuilder(lookup)();
                var cache = new InstanceBinding(Target, instance);
                depChain.Add(Target, cache);
                var injector = injection.GenInjector(lookup);
                injector(instance);

                depChain.Remove(Target);
                return instance;
            };
        }
    }
}