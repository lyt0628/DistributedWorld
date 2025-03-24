
using QS.GameLib.Util;

namespace QS.Combat.Domain
{
    sealed class LinearBuff : IBuff
    {
        public float apSlop { get; private set; } = 1f;
        public float apIntercept { get; private set; } = 0f;
        public float mpSlop { get; private set; } = 1f;
        public float mpIntercept { get; private set; } = 0;
        public float defSlop { get; private set; } = 1;
        public float defIntercept { get; private set; } = 0;
        public float mdefSlop { get; private set; } = 1;
        public float mdefIntercept { get; private set; } = 0;

        public LinearBuff(float apSlop,
                          float apIntercept,
                          float mpSlop,
                          float mpIntercept,
                          float defSlop,
                          float defIntercept,
                          float mdefSlop,
                          float mdefIntercept)
        {
            this.apSlop = apSlop;
            this.apIntercept = apIntercept;
            this.mpSlop = mpSlop;
            this.mpIntercept = mpIntercept;
            this.defSlop = defSlop;
            this.defIntercept = defIntercept;
            this.mdefSlop = mdefSlop;
            this.mdefIntercept = mdefIntercept;
        }

        public string UUID => MathUtil.UUID();

        public float AttackPowerPlus(float original, float acc)
        {
            return LinearPlus(original, acc, apSlop, apIntercept);
        }


        public float DefencePlus(float original, float acc)
        {
            return LinearPlus(original, acc, defSlop, defIntercept);
        }

        public float MagicDefencePlus(float original, float acc)
        {
            return LinearPlus(original, acc, mdefSlop, mdefIntercept);
        }

        public float MagicPowerPlus(float original, float acc)
        {
            return LinearPlus(original, acc, mpSlop, mpIntercept);
        }

        private float LinearPlus(float original, float acc, float slop, float intercept)
        {
            return acc + slop * original + intercept;
        }
    }
}
