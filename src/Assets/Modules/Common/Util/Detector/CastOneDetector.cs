using QS.GameLib.Util.Raycast;
using UnityEngine;

namespace QS.Common.Util.Detector
{
    class CastOneDetector : AbstractCastDetector
    {
        public CastOneDetector() { }
        public CastOneDetector(ICastedObject obj) : base(obj) { }

        protected override GameObject[] DoDetect(ICastedObject castedObject)
        {
            var o = RaycastHelper.Of(castedObject).Object;
            if (o != null)
            {
                return new GameObject[] { o };
            }
            else
            {
                return new GameObject[] { };
            }

        }
    }
}