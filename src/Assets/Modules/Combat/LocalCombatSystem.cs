

namespace QS.Combat
{
    class LocalCombatSystem : CombatSystem
    {

        public override void TakeDamage(CombatorBehaviour attacked, CombatorBehaviour attacker, ISkill skill)
        {
            // ����ֱ�Ӱ��˺�����Ӧ�����
            attacked.TakeDamage(attacker.Combator.AttackPower);
            attacker.OnHit.Invoke();
        }

        public override void TakeHeal(CombatorBehaviour combator, float hpDelta)
        {
            combator.TakeHeal(hpDelta);
        }
    }
}