


using GameLib.Pattern;

namespace QS.Domain.Combat
{
    public class AttackBuff : AbstractBuff
    {
        private readonly float atnBuff = 0;
        private readonly float matkBuff = 0;

        public AttackBuff(float atnBuff, float matkBuff)
        {
            this.atnBuff = atnBuff;
            this.matkBuff = matkBuff;
        }
        public override BuffStages AttackStage => BuffStages.Attack;

        public override void Read(IPipelineHandlerContext context, object msg)
        {
            var atk = msg as AttackMsg;
            atk.atn_res = atk.atn * atnBuff;
            atk.matk_res = atk.matk * matkBuff;
        }
    }
}
