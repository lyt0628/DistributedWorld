

using UnityEngine;

namespace GameLib.Util.Raycast
{
    public class RaycastHelper
    {
        private readonly ICastedObject _castedObject;
        private bool needUpdate = true;
        private bool _hit = false;
        public bool Hit { get { return _hit; } }
        private RaycastHit hitInfo;
        private RaycastHelper(ICastedObject castedObject)
        {
            _castedObject = castedObject;
        }

        public static RaycastHelper Of(ICastedObject castedObject)
        {
            return new RaycastHelper(castedObject);
        }
        public void Update()
        {
            needUpdate = true;
        }

        
       public float Distance()
        {
            UpdateIfNeed();
            return Hit? hitInfo.distance : float.PositiveInfinity;
        }

        public bool IsCloserThan(float d)
        {
            UpdateIfNeed();
            if(!Hit) return false;
            return Distance() < d;
         }

        public bool IsFartherThan(float d)
        {
            return !IsCloserThan(d);
        }
        private void UpdateIfNeed() {
            if (needUpdate) {
                needUpdate = false;
                if (_castedObject.Cast(out RaycastHit hit)) {
                    _hit = true;
                    hitInfo = hit;
                }
                else
                {
                    _hit = false;
                }
            }
        }
    }
}