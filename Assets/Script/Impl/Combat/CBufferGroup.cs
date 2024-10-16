using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace QS
{
    using QS.API;

    public class CBuffGroup : AbstractBuff
    {
        private List<IBuff> buffs = new List<IBuff> ();

        override public EBuffStage GetAttackStage()
        {
            return EBuffStage.None;
        }

        public override ICombatData BuffOnCombatData(ICombatData state)
        {
            return state;
        }
        public  override IAttack BuffOnAttack(IAttack attack)
        {
            return attack;
        }

        public override void DoBuff(IBuffable buffable)
        {
            foreach (IBuff buff in buffs)
            {
                buff.DoBuff(buffable);
            }
                
        }
    }
}