

using QS.GameLib.Util.Raycast;
using System.Collections.Generic;
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
        public IEnumerable<GameObject> Detect()
        {
            return DoDetect(castedObj);
        }
        protected abstract IEnumerable<GameObject> DoDetect(ICastedObject castedObject);

        public void SetCastedObject(ICastedObject obj)
        {
            castedObj = obj;
        }
    }
}