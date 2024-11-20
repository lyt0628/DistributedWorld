

using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLib.DI
{
    class MultipleInjection : AbstractInjection
    {
        private readonly ISet<IInjection> injections;
        public MultipleInjection(ISet<IInjection> injections) 
            : base(injections.SelectMany(inj=>inj.Dependencies).ToHashSet())
        {
            this.injections = injections;
        }

        public override Injector GenInjector(BindingLookup lookup)
        {
            return (object instance) =>
            {
                foreach (var injection in injections)
                {
                    injection.GenInjector(lookup)
                             .Invoke(instance);
                }
            };
        }
    }
}