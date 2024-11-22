using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;




namespace QS
{
    using QS.API;

    public class CAttack : IAttack {
        public float Atn{get;set;}
        public float Matk { get;set;}

        public static IAttack FromCombatData( ICombatData combatData)
        {
            var att = new CAttack
            {
                Atn = combatData.Atn,
                Matk = combatData.Matk
            };
            return att;
        }
        public static ICombatData ComputeCombatData(ICombatData combatData, IAttack attack)
        {
            var ret = combatData.Clone();
            var atnDamage = attack.Atn > combatData.Def ? 
                                    attack.Atn - combatData.Def : 1;
            var matkDamage = attack.Matk > combatData.Res ? 
                                attack.Matk - combatData.Res: 1;

            
            ret.Hp = combatData.Hp - atnDamage - matkDamage ;
            return ret;
        }
    }
}