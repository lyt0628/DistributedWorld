using System.Collections.Generic;

namespace GameLib.DI
{
    abstract class AbstractInjection : IInjection
    {
        protected ISet<Key> dependencies;
        public AbstractInjection(ISet<Key> dependencies)
        {
            this.dependencies = dependencies;
        }

        public ISet<Key> Dependencies => dependencies;

        public abstract Injector GenInjector(BindingLookup lookup);
    }
}
