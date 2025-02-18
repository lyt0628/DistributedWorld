

using QS.Api.Combat.Domain;
using QS.Api.Combat.Service;
using QS.Combat.Domain;

namespace QS.Combat.Service
{
    class AttackFactory : IAttackFactory
    {
        public IAttack NewAttack(float atk, float matk)
        {
            return new Attack(atk, matk);
        }
    }
}