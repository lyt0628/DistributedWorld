using GameLib.Pattern;
using GameLib.Util;

namespace QS.Domain.Combat
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