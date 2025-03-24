


using QS.Api.Common.Util.Detector;
using UnityEngine;

namespace QS.Common.Util.Detector
{
    interface ICollideDetector : ISpanDetector
    {
        void SetCollider(Collider collider);
    }
}
