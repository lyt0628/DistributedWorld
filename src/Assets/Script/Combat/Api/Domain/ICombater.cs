using System.Collections.Generic;

namespace QS.Api.Combat.Domain
{
    public interface ICombater : ICombatable, IAttackable, IInjurable
    {

        public bool Combating { get; set; }
        public List<ICombatable> Enemies { get; set; }

    }
}
