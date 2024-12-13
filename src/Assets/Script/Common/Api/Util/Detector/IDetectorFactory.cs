


using QS.GameLib.Rx.Relay;
using QS.GameLib.Util.Overlap;
using QS.GameLib.Util.Raycast;
using System.Collections.Generic;
using UnityEngine;

namespace QS.Api.Common.Util.Detector
{
    public interface IDetectorFactory
    {
        IDetector CastOne(ICastedObject castedObject);
        IDetector CastOneRelay(Relay<ICastedObject> objRelay);
        IDetector CastAll(ICastedObject castedObject);
        IDetector CastAllRelay(Relay<ICastedObject> objRelay);
        IDetector Overlap(IOverlapArea area);
        IDetector OverlapRelay(Relay<IOverlapArea> areaRelay);
        ISpanDetector Collide(Collider collider, CollideStage stage);
        ISpanDetector CollideRelay(Relay<Collider> colliderRelay, CollideStage stage);
        IDetector Group(IEnumerable<IDetector> detectors);

    }
}