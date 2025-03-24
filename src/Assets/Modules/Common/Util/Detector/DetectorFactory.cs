

using QS.Api.Common.Util.Detector;
using QS.GameLib.Rx.Relay;
using QS.GameLib.Util.Overlap;
using QS.GameLib.Util.Raycast;
using System.Collections.Generic;
using UnityEngine;

namespace QS.Common.Util.Detector
{
    class DetectorFactory : IDetectorFactory
    {
        public IDetector CastAll(ICastedObject castedObject)
        {
            return new CastAllDetector(castedObject);
        }

        public IDetector CastAllRelay(Relay<ICastedObject> objRelay)
        {
            var d = new CastAllDetector();
            return new RelayCastDetector(d, objRelay);
        }

        public IDetector CastOne(ICastedObject castedObject)
        {
            return new CastOneDetector(castedObject);
        }

        public IDetector CastOneRelay(Relay<ICastedObject> objRelay)
        {
            var d = new CastOneDetector();
            return new RelayCastDetector(d, objRelay);
        }

        public ISpanDetector Collide(Collider collider, CollideStage stage, bool useTrigger = true)
        {
            return new CollideDetector(collider, stage, useTrigger);
        }

        public ISpanDetector CollideRelay(Relay<Collider> colliderRelay, CollideStage stage, bool useTrigger = true)
        {
            var d = new CollideDetector(stage, useTrigger);
            return new RelayCollideDetector(d, colliderRelay);
        }

        public IDetector Group(IEnumerable<IDetector> detectors)
        {
            var g = new DetectorGroup();
            foreach (var detector in detectors)
            {
                g.Add(detector);
            }
            return g;
        }

        public IDetector Overlap(IOverlapArea area)
        {
            return new OverlapDetector(area);
        }

        public IDetector OverlapRelay(Relay<IOverlapArea> areaRelay)
        {
            var d = new OverlapDetector();
            return new RelayOverlapDetector(d, areaRelay);
        }
    }
}