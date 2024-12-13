using QS.GameLib.Rx.Relay;
using QS.GameLib.Util.Raycast;
using System.Collections.Generic;
using UnityEngine;

namespace QS.Common.Util.Detector
{
    class CastAllDetector : AbstractCastDetector
    {
        public CastAllDetector() { }
        public CastAllDetector(ICastedObject objs) : base(objs){}

        protected override IEnumerable<GameObject> DoDetect(ICastedObject castedObject)
        {
            return RaycastHelperAll.Of(castedObject).Objects;
        }
    }

}