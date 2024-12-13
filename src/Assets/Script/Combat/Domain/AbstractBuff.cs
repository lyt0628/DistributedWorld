using QS.Api.Combat.Domain;
using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Util;

namespace QS.Combat.Domain
{

    public abstract class AbstractBuff<T> : InBoundPipelineHandlerAdapter, IBuff 
    {
        protected AbstractBuff()
        {
            Id = MathUtil.UUID();
        }

        public abstract BuffStages AttackStage { get; }
        public string Id { get; }

        public override void Read(IPipelineHandlerContext context, object msg)
        {
            var m = MakeBuff((T)msg);
            context.Write(m);
        }

        protected abstract object MakeBuff(T msg);
    }
}