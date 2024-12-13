

using QS.Api.Common.Util.Detector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QS.Common.Util.Detector
{
    class DetectorGroup : IDetector
    {
        readonly List<IDetector> detectors = new();
        public void Add(IDetector detector)
        {
            if (detectors.Contains(detector))
            {
                Debug.LogWarning("Detector Group Aleady Contains");
                return;
            }
            detectors.Add(detector);
        }

        public IEnumerable<GameObject> Detect()
        {
            return detectors
                        .SelectMany(d=>d.Detect())
                        .Distinct()
                        .ToList();
        }

    }
}