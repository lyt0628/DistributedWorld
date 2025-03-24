

namespace QS.Combat
{
    class LocalCombatSystem : CombatSystem
    {

        public override void TakeDamage(CombatorBehaviour attacked, CombatorBehaviour attacker, ISkill skill)
        {
            // 现在直接把伤害给对应的组件
            attacked.TakeDamage(attacker.Combator.AttackPower);
            attacker.OnHit.Invoke();
        }

        public override void TakeHeal(CombatorBehaviour combator, float hpDelta)
        {
            combator.TakeHeal(hpDelta);
        }
    }
}