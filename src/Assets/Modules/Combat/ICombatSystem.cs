


namespace QS.Combat
{
    public interface ICombatSystem
    {
        void TakeDamage(CombatorBehaviour attacked, CombatorBehaviour attacker, ISkill skill);
        void TakeHeal(CombatorBehaviour combator, float hpDelta);
    }
}