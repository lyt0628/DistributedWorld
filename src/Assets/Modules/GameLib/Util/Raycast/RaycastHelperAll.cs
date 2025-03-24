



using System.Linq;
using UnityEngine;

namespace QS.GameLib.Util.Raycast
{

    public class RaycastHelperAll
    {
        readonly ICastedObject _castedObject;
        bool needUpdate = true;
        bool _hit = false;
        public RaycastHit[] m_HitInfos;
        private RaycastHelperAll(ICastedObject castedObject)
        {
            _castedObject = castedObject;
        }


        public static RaycastHelperAll Of(ICastedObject castedObject)
        {
            return new RaycastHelperAll(castedObject);
        }

        public bool Hit
        {
            get
            {
                UpdateIfNeed();
                return _hit;
            }
        }

        public RaycastHit[] HitInfos
        {
            get
            {
                UpdateIfNeed();
                return m_HitInfos;
            }
        }

        public GameObject[] Objects
        {
            get
            {
                UpdateIfNeed();
                return m_HitInfos.Select(i => i.collider.gameObject).ToArray();
            }
        }

        public Collider[] AllColliders
        {
            get
            {
                UpdateIfNeed();
                return m_HitInfos.Select(i => i.collider).ToArray();
            }
        }

        void UpdateIfNeed()
        {
            if (!needUpdate) return;
            needUpdate = false;

            m_HitInfos = _castedObject.CastAll();
            _hit = m_HitInfos.Length == 0;
        }
    }
}