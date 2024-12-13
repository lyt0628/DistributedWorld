


using QS.Api.Common.Util.Detector;
using QS.GameLib.Util.Overlap;
using System.Collections.Generic;
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

        public IEnumerable<GameObject> Detect()
        {
            return area.Overlap().Select(collider => collider.gameObject);
        }

        public void SetOverlapArea(IOverlapArea area)
        {
            this.area = area;
        }
    }
}