using QS.GameLib.Util.Raycast;
using UnityEngine;

namespace QS.Common.Util.Detector
{
    class CastAllDetector : AbstractCastDetector
    {
        public CastAllDetector() { }
        public CastAllDetector(ICastedObject objs) : base(objs) { }

        protected override GameObject[] DoDetect(ICastedObject castedObject)
        {
            return RaycastHelperAll.Of(castedObject).Objects;
        }
    }

}