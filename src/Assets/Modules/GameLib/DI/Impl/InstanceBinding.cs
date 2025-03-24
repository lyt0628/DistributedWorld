namespace GameLib.DI
{
    class InstanceBinding : AbstractBinding
    {
        /// <summary>
        /// The instance of this binding
        /// </summary>
        private readonly object instance;


        public InstanceBinding(Key target, object instance)
            : base(target, Bindings.EmptyDeps)
        {
            this.instance = instance;
        }

        public override Builder GenBuilder(BindingLookup lookup)
        {
            return () => instance;
        }
    }
}
