using System;
using System.Collections.Generic;

namespace QS.Api.Combat.Domain
{
    /// <summary>
    /// Combat Data is a value type.
    /// </summary>
    public class CombatData : ICombatData
    {
        public float Hp { get; set; }
        public float Mp { get; set; }
        public float Atn { get; set; }
        public float Def { get; set; }
        public float Matk { get; set; }
        public float Res { get; set; }

        public ICombatData Clone()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return obj is CombatData data &&
                   Hp == data.Hp &&
                   Mp == data.Mp &&
                   Atn == data.Atn &&
                   Def == data.Def &&
                   Matk == data.Matk &&
                   Res == data.Res;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Hp, Mp, Atn, Def, Matk, Res);
        }

        public static bool operator ==(CombatData left, CombatData right)
        {
            return EqualityComparer<CombatData>.Default.Equals(left, right);
        }

        public static bool operator !=(CombatData left, CombatData right)
        {
            return !(left == right);
        }

        public static CombatData operator +(CombatData left, CombatData right)
        {
            return new CombatData()
            {
                Atn = left.Atn + right.Atn,
                Def = left.Def + right.Def,
                Matk = left.Matk + right.Matk,
                Res = left.Res + right.Res,
                Hp = left.Hp + right.Hp,
                Mp = left.Mp + right.Mp,
            };
        }

        public static CombatData operator -(CombatData left, CombatData right)
        {
            return new CombatData()
            {
                Atn = left.Atn - right.Atn,
                Def = left.Def - right.Def,
                Matk = left.Matk - right.Matk,
                Res = left.Res - right.Res,
                Hp = left.Hp - right.Hp,
                Mp = left.Mp - right.Mp,
            };
        }
    }
}