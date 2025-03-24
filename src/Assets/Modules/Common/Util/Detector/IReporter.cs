


using System.Collections.Generic;

namespace QS.Common.Util.Detector
{
    interface IReporter<T>
    {
        IEnumerable<T> Report();
        string UUID { get; }
    }
}