using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QS.API;
using QS;
using QS.API.Service.Domain;

public class CABuff : AbstractBuff
{
    public CABuff(ICombatData data)
        : base(data)
    {
    }
    override public BuffStages AttackStage => BuffStages.IMMEDIATE;
    public override ICombatData  BuffOnCombatData(ICombatData state)
     {
        var result = state.Clone();
        result.Atn += Data.Atn;
        result.Def += Data.Def;
        result.Matk += Data.Matk;
        result.Res += Data.Res;

        return result;
     }

    public new IBuff Clone()
    {
        var data =  Data.Clone();
        return new CABuff(data);
    }
}
