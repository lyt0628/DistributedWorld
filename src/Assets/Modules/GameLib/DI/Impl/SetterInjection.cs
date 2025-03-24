using System.Collections.Generic;

namespace GameLib.DI
{
    class SetterInjection : AbstractInjection
    {
        private readonly SpecSetter setter;
        private readonly Key typeKey;
        public SetterInjection(SpecSetter setter, Key dependency)
            : base(new HashSet<Key>() { dependency })
        {
            this.setter = setter;
            this.typeKey = dependency;
        }

        public override Injector GenInjector(BindingLookup lookup)
        {
            return (instance) =>
            {
                var value = lookup(typeKey)
                            .GenBuilder(lookup)();
                setter(instance, value);
            };
        }
    }
}