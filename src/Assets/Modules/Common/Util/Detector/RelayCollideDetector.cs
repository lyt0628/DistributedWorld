

using QS.Api.Common.Util.Detector;
using QS.GameLib.Rx.Relay;
using UnityEngine;

namespace QS.Common.Util.Detector
{
    class RelayCollideDetector : ISpanDetector
    {
        readonly ICollideDetector detector;
        public RelayCollideDetector(ICollideDetector detector, Relay<Collider> colliderRelay)
        {
            this.detector = detector;
            colliderRelay.Subscrib((c) => detector.SetCollider(c));
        }

        public bool Enabled => detector.Enabled;

        public GameObject[] Detect()
        {
            return detector.Detect();
        }

        public void Disable()
        {
            detector.Disable();
        }

        public void Enable()
        {
            detector.Enable();
        }
    }
}