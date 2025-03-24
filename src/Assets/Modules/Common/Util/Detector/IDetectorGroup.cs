

using QS.Api.Common.Util.Detector;

namespace QS.Common.Util.Detector
{
    interface IDetectorGroup : IDetector
    {
        public void Add(IDetector detector);
    }
}