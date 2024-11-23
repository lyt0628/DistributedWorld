using System.Collections.Generic;
using System.Linq;

namespace GameLib.DI
{
    class ConstructorBinding : AbstractBinding
    {
        private readonly Construct construct;
        private readonly ISet<Key> arguments;

        public ConstructorBinding(Key target,
                                    Construct constructor,
                                    ISet<Key> dependencies)
            : base(target, dependencies)
        {
            this.construct = constructor;
            arguments = dependencies;
        }

        public override Builder GenBuilder(BindingLookup lookup)
        {
            var args = arguments
                .Select(key => lookup(key))
                .Select(binding => binding.GenBuilder(lookup)())
                .ToArray();
            return () => construct(args);
        }
    }
}
