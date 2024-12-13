using UnityEngine;

namespace QS.GameLib.Util.Raycast
{
    public class RaycastHelper
    {
        readonly ICastedObject _castedObject;
        bool needUpdate = true;
        bool _hit = false;
        RaycastHit hitInfo;


        public bool Hit { 
            get 
            {
                UpdateIfNeed();
                return _hit; 
            } 
        }
        public float Distance 
        {
            get
            {
                UpdateIfNeed();
                return Hit ? hitInfo.distance : float.PositiveInfinity;
            }
        }
        public Vector3 Normal
        {
            get
            {
                UpdateIfNeed();
                return hitInfo.normal;
            }
        }
        public GameObject Object
        {
            get
            {
                UpdateIfNeed();
                return _hit ? hitInfo.collider.gameObject : null;
            }
        }
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
       

        public bool IsCloserThan(float d)
        {
            UpdateIfNeed();
            if (!Hit) return false;
            return Distance < d;
        }
        public bool IsCloserThanOrJust(float d)
        {
            UpdateIfNeed();
            if (!Hit) return false;
            return Distance <= d;
        }
        public bool IsFartherThanOrJust(float d)
        {
            UpdateIfNeed();
            if (!Hit) return false;
            return Distance >= d;        }

        void UpdateIfNeed()
        {
            if(!needUpdate) return;
            needUpdate = false;

            if (_castedObject.Cast(out RaycastHit hit))
            {
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