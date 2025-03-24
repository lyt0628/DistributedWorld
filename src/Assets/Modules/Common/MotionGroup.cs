


using QS.GameLib.Rx.Relay;
using System.Collections.Generic;

namespace QS.Common
{
    public class MotionGroup : IMotion
    {
        readonly List<IMotion> motions = new();
        public void Add(IMotion motion)
        {
            motions.Add(motion);
        }

        public void Set()
        {
            motions.ForEach(motion => motion.Set());
        }
    }
}