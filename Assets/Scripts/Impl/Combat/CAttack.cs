


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
        public static ICombatData ComputeCombatData(ICombatData combatData)
        {
            return combatData;
        }
    }
}