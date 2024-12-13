

using QS.Api.Combat.Domain;

namespace QS.Api.Combat.Service
{
    public interface IBuffFactory
    {
        IBuff AttackBuff(float atk, float matk);

    }
}