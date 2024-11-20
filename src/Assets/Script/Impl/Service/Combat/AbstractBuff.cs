namespace QS
{
    using QS.API;
    using GameLib;
    public abstract class AbstractBuff : IBuff
    {
        public IBuffData Data { get; set; }

        public abstract EBuffStage GetAttackStage() ;

        public virtual ICombatData BuffOnCombatData(ICombatData state) {  return state; }
        public virtual IAttack BuffOnAttack(IAttack attack) { return attack; }

        public virtual void DoBuff(IBuffable buffable)
        {
            buffable.AcceptBuff(this);
        }

        public IBuff Clone() { throw new System.Exception(); }
    }
}