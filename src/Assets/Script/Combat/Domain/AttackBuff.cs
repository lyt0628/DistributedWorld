using QS.Api.Combat.Domain;
using QS.GameLib.Pattern.Pipeline;

namespace QS.Combat.Domain
{
    class AttackBuff : AbstractBuff<AttackMsg>
    {
         public float AtkRatio { get; }
         public float MatkRatio { get; }

        public AttackBuff(float atnBuff, float matkBuff)
        {
            this.AtkRatio = atnBuff;
            this.MatkRatio = matkBuff;
        }

        public override BuffStage Stage => BuffStage.Attack;


        protected override object MakeBuff(AttackMsg msg)
        {
            msg.atn_res = msg.atn * AtkRatio;
            msg.matk_res = msg.matk * MatkRatio;
            return msg;
        }
    }
}
