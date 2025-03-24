using System.Collections.Generic;

namespace GameLib.DI
{
    abstract class AbstractBinding : IBinding
    {
        readonly ISet<Key> dependencies;

        /// <summary>
        /// Key for souce class that is this binding found from. 
        /// For example:
        /// <code>
        ///   class A{}
        ///   
        ///   var ctx = IDIContext.new();
        ///   ctx.Bind<A>()
        /// </code>
        /// 
        /// And the key is id for the class.
        /// default Name of Key is <code>nameof(A)</code>, And Type is <code>typeof(A)</code>
        /// </summary>
        readonly Key source;

        public AbstractBinding(Key target,
                            ISet<Key> dependencies,
                            ScopeFlag scope = ScopeFlag.Sington,
                            bool lazy = true,
                            int priority = 0)
        {
            this.source = target;
            this.dependencies = dependencies;
            Scope = scope;
            Priority = priority;
            Lazy = lazy;
        }

        /// <summary>
        /// 綁定的實際類型鍵， 即實例類的Key
        /// </summary>
        public Key Target => source;

        public ISet<Key> Dependencies => dependencies;

        public ScopeFlag Scope { get; set; }

        public bool IsSington => Scope == ScopeFlag.Sington || Scope == ScopeFlag.Default;

        public bool IsPrototype => Scope == ScopeFlag.Prototype;

        public int Priority { get; set; }
        public bool Lazy { get; set; }

        public abstract Builder GenBuilder(BindingLookup lookup);
    }
}
