


namespace QS.Api.Common.Util.Detector
{
    public interface ISpanDetector : IDetector
    {
        void Enable();
        void Disable();
        bool Enabled { get; }
    }
}