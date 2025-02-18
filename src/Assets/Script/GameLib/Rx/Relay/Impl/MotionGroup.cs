


using System.Collections.Generic;
using System.Linq;

namespace QS.GameLib.Rx.Relay
{
    class MotionGroup : IMotion
    {
        readonly List<IDisposableMotion> motions = new();
        public void Add(IDisposableMotion motion)
        {
            motions.Add(motion);
        }

        public void Set()
        {
            motions.RemoveAll(motion =>motion.Disposed);
            motions
                .Where(motion => !motion.Paused)
                .ToList()
                .ForEach(motion=> motion.Set());
        }
    }
}