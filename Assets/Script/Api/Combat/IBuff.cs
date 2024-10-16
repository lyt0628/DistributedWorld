using GameLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QS.API
{

    public interface IBuff : IClonable<IBuff>
    {
        public EBuffStage GetAttackStage();
        public ICombatData BuffOnCombatData(ICombatData state);
        public IAttack BuffOnAttack(IAttack attack);

        public void DoBuff(IBuffable buffable);


    }
}
