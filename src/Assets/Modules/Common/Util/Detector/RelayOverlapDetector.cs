

using QS.Api.Common.Util.Detector;
using QS.GameLib.Rx.Relay;
using QS.GameLib.Util.Overlap;
using UnityEngine;

namespace QS.Common.Util.Detector
{
    class RelayOverlapDetector : IDetector
    {
        readonly IOverlapDetector detector;
        public RelayOverlapDetector(IOverlapDetector detector, Relay<IOverlapArea> areaRelay)
        {
            this.detector = detector;
            areaRelay.Subscrib((area) => detector.SetOverlapArea(area));
        }

        public GameObject[] Detect()
        {
            return detector.Detect();
        }
    }
}