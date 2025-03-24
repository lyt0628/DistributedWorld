

using QS.GameLib.Util.Raycast;
using UnityEngine;

namespace QS.Common.Util.Detector
{
    abstract class AbstractCastDetector : ICastDetector
    {
        ICastedObject castedObj;
        public AbstractCastDetector()
        {
        }

        public AbstractCastDetector(ICastedObject castedObj)
        {
            this.castedObj = castedObj;
        }
        public GameObject[] Detect()
        {
            return DoDetect(castedObj);
        }
        protected abstract GameObject[] DoDetect(ICastedObject castedObject);

        public void SetCastedObject(ICastedObject obj)
        {
            castedObj = obj;
        }
    }
}