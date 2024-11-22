
using QS.API;
using GameLib;
using System;

namespace QS.API.Service.Domain
{

    public abstract class AbstractBuff : IBuff
    {
        public AbstractBuff(ICombatData data)
        {
            Data = data;
        }
        protected ICombatData Data { get; set; }

        public abstract BuffStages AttackStage { get; }

        public virtual ICombatData BuffOnCombatData(ICombatData state) 
        {  return state; }
        public virtual IAttack BuffOnAttack(IAttack attack) 
        { return attack; }

        public virtual void DoBuff(IBuffable buffable)
        {
            buffable.AcceptBuff(this);
        }

        public IBuff Clone() { throw new NotSupportedException(); }
    }
}