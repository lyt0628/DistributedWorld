



using System.Linq;
using UnityEngine;

namespace QS.GameLib.Util.Raycast
{

    public class RaycastHelperAll
    {        
        readonly ICastedObject _castedObject;
        bool needUpdate = true;
        bool _hit = false;
        RaycastHit[] hitAllInfo;
         private RaycastHelperAll(ICastedObject castedObject)
        {
            _castedObject = castedObject;
        }


        public static RaycastHelperAll Of(ICastedObject castedObject)
        {
            return new RaycastHelperAll(castedObject);
        }

        public bool Hit { 
            get 
            {
                UpdateIfNeed();
                return _hit; 
            } 
        }
        
        public GameObject[] Objects
        {
            get
            {
                UpdateIfNeed();
                return hitAllInfo.Select(i =>i.collider.gameObject).ToArray();
            }
        }

        public Collider[] AllColliders
        {
            get
            {
                UpdateIfNeed();
                return hitAllInfo.Select(i =>i.collider).ToArray();
            }
        }

        void UpdateIfNeed()
        {
            if(!needUpdate) return;
            needUpdate = false;

            hitAllInfo = _castedObject.CastAll();
            _hit = hitAllInfo.Length == 0;
        }
    }
}