using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QS.API;
using QS;

public class CABuff : AbstractBuff
{
    public CABuff() { }
    public CABuff(IBuffData data)
    {
        Data = data;
    }
      override  public EAttackStage GetAttackStage()
      {
        return EAttackStage.BEFORE_ATTACK;
      }
     public override ICombatData  BuffOnCombatData(ICombatData state)
     {
        var result = state.Clone();
        result.Atn += Data.Atn;
        result.Def += Data.Def;
        result.Matk += Data.Matk;
        result.Res += Data.Res;

        Debug.Log(string.Format("FFFAtk: {0}, Matk : {1}", Data.Atn, Data.Matk));
        Debug.Log(string.Format("666Atk: {0}, Matk : {1}", result.Atn, result.Matk));
        return result;
     }

    public new IBuff Clone()
    {
        var data =  Data.Clone();
        return new CABuff(data);
    }
}
