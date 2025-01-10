


using QS.Api.Executor.Domain;
using QS.Executor.Domain;

namespace QS.Executor
{
    /// <summary>
    /// I am soryy change the name from 'Handler' to 'Abilty', but we does need a shorter and doamin
    /// specific name for this class.
    /// </summary>
    public abstract class BehaviourHandler : AbstractHandler
    {
        protected BehaviourHandler(ExecutorBehaviour behaviour) : base(behaviour)
        {
            Behaviour = behaviour;
        }

        public ExecutorBehaviour Behaviour { get; }

    }
}
