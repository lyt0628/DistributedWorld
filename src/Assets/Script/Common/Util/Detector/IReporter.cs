


using System.Collections.Generic;
using UnityEngine;

namespace QS.Common.Util.Detector
{
    interface IReporter<T>
    {
        IEnumerable<T> Report();
        string UUID { get; }
    }
}