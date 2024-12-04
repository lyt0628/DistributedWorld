using QS.Api.Combat.Domain;
using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Util;

namespace QS.Combat.Domain
{

    public abstract class AbstractBuff : InBoundPipelineHandlerAdapter, IBuff
    {
        protected AbstractBuff()
        {
            Id = MathUtil.UUID();
        }
        public abstract BuffStages AttackStage { get; }
        public string Id { get; }

    }
}