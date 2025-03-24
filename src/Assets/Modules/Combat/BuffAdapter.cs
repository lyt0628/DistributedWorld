
using QS.GameLib.Util;

namespace QS.Combat.Domain
{

    public abstract class BuffAdapter : IBuff
    {


        public string UUID { get; } = MathUtil.UUID();


        public virtual float AttackPowerPlus(float original, float acc) => acc;

        public virtual float DefencePlus(float original, float acc) => acc;

        public virtual float MagicDefencePlus(float original, float acc) => acc;

        public virtual float MagicPowerPlus(float original, float acc) => acc;
    }
}