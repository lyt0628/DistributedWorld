


using UnityEngine;

namespace QS.GameLib.Util.Overlap
{
    public abstract class OverlapArea : IOverlapArea
    {
        public int _layerMask = -5;
        public QueryTriggerInteraction _triggerInteraction = QueryTriggerInteraction.UseGlobal;

        public IOverlapArea IgnoreTrigger(bool ignore = true)
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


        public IOverlapArea LayerMask(int mask)
        {
            _layerMask = mask;
            return this;
        }

        public IOverlapArea TriggerInteraction(QueryTriggerInteraction triggerInteraction)
        {
            _triggerInteraction = triggerInteraction;
            return this;
        }

        public Collider[] Overlap()
        {
            return DoOverlap(_layerMask, _triggerInteraction);
        }

        public abstract Collider[] DoOverlap(int layermask, QueryTriggerInteraction triggerInteraction);

        public static IOverlapArea Sphere(Vector3 center, float radius)
        {
            return new SphereArea(center, radius);
        }

        class SphereArea : OverlapArea
        {
            Vector3 center;
            readonly float radius;
            public SphereArea(Vector3 center, float radius)
            {
                this.center = center;
                this.radius = radius;
            }
            public override Collider[] DoOverlap(int layermask, QueryTriggerInteraction triggerInteraction)
            {
                return Physics.OverlapSphere(center, radius, layermask, triggerInteraction);
            }
        }
    }
}
