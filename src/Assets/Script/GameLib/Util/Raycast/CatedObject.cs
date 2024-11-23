using GameLib.Util.Raycast;

using UnityEngine;


namespace GameLib.Uitl.RayCast
{

    public abstract class CastedObject : ICastedObject
    {

        private float _maxDistance = float.PositiveInfinity;
        public int _layerMask = -5;
        public QueryTriggerInteraction _triggerInteraction = QueryTriggerInteraction.UseGlobal;

        public bool Cast(out RaycastHit hitInfo)
        {

            return CastOne(_maxDistance, _layerMask, _triggerInteraction, out hitInfo);
        }
        public RaycastHit[] CastAll()
        {
            return CastEvery(_maxDistance, _layerMask, _triggerInteraction);
        }

        protected abstract bool CastOne(float maxDistance, int layermask, QueryTriggerInteraction triggerInteraction, out RaycastHit hitInfo);
        protected abstract RaycastHit[] CastEvery(float maxDistance, int layermask, QueryTriggerInteraction triggerInteraction);

        public static ICastedObject Ray(Vector3 pos, Vector3 direction)
        {
            return new CastedRay(pos, direction);
        }

        public static ICastedObject Capsule(Vector3 point1, Vector3 point2, float radius, Vector3 direction)
        {
            return new CastedCapsule(point1, point2, radius, direction);
        }

        public ICastedObject IgnoreTrigger(bool ignore = true)
        {
            if (ignore)
            {
                _triggerInteraction = QueryTriggerInteraction.Ignore;
            }
            else
            {
                _triggerInteraction = QueryTriggerInteraction.Collide;
            }

            return this;
        }

        public ICastedObject MaxDistance(float distance)
        {
            this._maxDistance = distance;
            return this;
        }

        public ICastedObject LayerMask(int mask)
        {
            this._layerMask = mask;
            return this;
        }

        public ICastedObject TriggerInteraction(QueryTriggerInteraction triggerInteraction)
        {
            _triggerInteraction = triggerInteraction;
            return this;
        }

        internal class CastedRay : CastedObject
        {
            private Vector3 _position;
            private Vector3 _direction;
            public CastedRay(Vector3 position, Vector3 direction)
            {
                _position = position;
                _direction = direction;
            }
            protected override bool CastOne(float maxDistance, int layermask, QueryTriggerInteraction triggerInteraction, out RaycastHit hitInfo)
            {
                return Physics.Raycast(_position, _direction, out hitInfo, maxDistance, layermask, triggerInteraction);
            }

            protected override RaycastHit[] CastEvery(float maxDistance, int layermask, QueryTriggerInteraction triggerInteraction)
            {
                return Physics.RaycastAll(_position, _direction, maxDistance, layermask, triggerInteraction);
            }
        }


        internal class CastedCapsule : CastedObject
        {
            private Vector3 _point1;
            private Vector3 _point2;
            private float _radius;
            private Vector3 _direction;
            public CastedCapsule(Vector3 point1, Vector3 point2, float radius, Vector3 direction)
            {
                _point1 = point1;
                _point2 = point2;
                _radius = radius;
                _direction = direction;
            }
            protected override RaycastHit[] CastEvery(float maxDistance, int layermask, QueryTriggerInteraction triggerInteraction)
            {
                return Physics.CapsuleCastAll(_point1, _point2, _radius, _direction, maxDistance, layermask, triggerInteraction);
            }

            protected override bool CastOne(float maxDistance, int layermask, QueryTriggerInteraction triggerInteraction, out RaycastHit hitInfo)
            {
                return Physics.CapsuleCast(_point1, _point2, _radius, _direction, out hitInfo, maxDistance, layermask, triggerInteraction);
            }
        }



    }

}