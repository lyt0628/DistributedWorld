

using QS.Api.Common.Util.Detector;
using QS.GameLib.Util.Overlap;

namespace QS.Common.Util.Detector
{
    interface IOverlapDetector : IDetector
    {
        void SetOverlapArea(IOverlapArea area);
    }

}