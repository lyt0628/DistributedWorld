


using System.Collections.Generic;

namespace QS.GameLib.Rx.Relay
{
    class MotionGroup : IMotion
    {
        readonly List<IMotion> motions = new();
        public void Add(IMotion motion)
        {
            motions.Add(motion);
        }

        public void Set()
        {
            motions.ForEach(m=>m.Set());
        }
    }
}