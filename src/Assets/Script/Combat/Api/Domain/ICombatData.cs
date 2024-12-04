namespace QS.Api.Combat.Domain
{
    using QS.GameLib.Pattern;

    /*
     * TODO: Make ICombatData all accessors   Pure Functions
     */

    public interface ICombatData : IClonable<ICombatData>
    {
        public float Hp { get; set; }
        public float Mp { get; set; }
        public float Atn { get; set; }
        public float Def { get; set; }
        public float Matk { get; set; }
        public float Res { get; set; }

    }

}
