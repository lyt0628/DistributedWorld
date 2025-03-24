

using QS.Api.Common.Util.Detector;
using QS.GameLib.Util.Raycast;

namespace QS.Common.Util.Detector
{
    interface ICastDetector : IDetector
    {
        void SetCastedObject(ICastedObject obj);
    }
}

