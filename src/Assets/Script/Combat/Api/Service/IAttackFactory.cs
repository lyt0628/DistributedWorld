

using QS.Api.Combat.Domain;

namespace QS.Api.Combat.Service
{
    public interface IAttackFactory
    {
        IAttack NewAttack(float atk, float matk);
    }
}