namespace QS.Api.Combat.Domain
{
    using QS.GameLib.Pattern;

    /*
     * TODO: Make ICombatData all accessors   Pure Functions
     */

    public interface ICombatData : IClonable<ICombatData>
    {
        public float Hp { get; }
        public float Mp { get; }
        public float Atn { get; }
        public float Def { get; }
        public float Matk { get; }
        public float Res { get; }

    }

}
