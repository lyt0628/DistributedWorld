using QS.GameLib.Util.Overlap;
using System.Linq;
using UnityEngine;

namespace QS.Common.Util.Detector
{
    class OverlapDetector : IOverlapDetector
    {
        IOverlapArea area;
        public OverlapDetector() { }
        public OverlapDetector(IOverlapArea area)
        {
            this.area = area;
        }

        public GameObject[] Detect()
        {
            return area.Overlap()
                .Select(collider => collider.gameObject)
                .ToArray();
        }

        public void SetOverlapArea(IOverlapArea area)
        {
            this.area = area;
        }
    }
}