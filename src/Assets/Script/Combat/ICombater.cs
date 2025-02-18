using System.Collections.Generic;

namespace QS.Api.Combat.Domain
{
    public interface ICombater : IInjurable, IAttackable
    {
        public ICombatData CombatData { get; }
        public ICombatData MaxCombatData { get; }
    }
}
