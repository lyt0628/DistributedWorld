

using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLib.DI
{
    class InjectedBinding : AbstractBinding
    {
        private readonly IInjection injection;
        private readonly IBinding rawBinding;
        public InjectedBinding(IBinding rawBinding,
                               IInjection injection) 
            : base(rawBinding.Target, 
                  rawBinding.Dependencies
                  .Union(injection.Dependencies)
                  .ToHashSet(), 
                  rawBinding.Scope)
        {
            this.injection = injection;
            this.rawBinding = rawBinding;
        }

        public override Builder GenBuilder(BindingLookup lookup)
        {
            return () =>
            {
                var instance = rawBinding.GenBuilder(lookup)();
                var injector = injection.GenInjector(lookup);
                injector(instance);

                return instance;
            };
        }
    }
}