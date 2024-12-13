

using QS.Api.Combat.Domain;
using QS.Api.Combat.Service;
using QS.Combat.Domain;

namespace QS.Combat.Service
{
    class BuffFactory : IBuffFactory
    {
        public IBuff AttackBuff(float atk, float matk)
        {
            return new AttackBuff(atk, matk);
        }
    }
}