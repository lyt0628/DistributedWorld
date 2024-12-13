using QS.Api.Combat.Domain;
using QS.GameLib.Pattern.Pipeline;

namespace QS.Combat.Domain
{
    class AttackBuff : AbstractBuff<AttackMsg>
    {
        private readonly float atnBuff = 0;
        private readonly float matkBuff = 0;

        public AttackBuff(float atnBuff, float matkBuff)
        {
            this.atnBuff = atnBuff;
            this.matkBuff = matkBuff;
        }

        public override BuffStages AttackStage => BuffStages.Attack;


        protected override object MakeBuff(AttackMsg msg)
        {
            msg.atn_res = msg.atn * atnBuff;
            msg.matk_res = msg.matk * matkBuff;
            return msg;
        }
    }
}
