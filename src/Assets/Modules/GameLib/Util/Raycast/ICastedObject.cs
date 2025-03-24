

using UnityEngine;

namespace QS.GameLib.Util.Raycast
{
    public interface ICastedObject
    {
        public ICastedObject MaxDistance(float distance);
        public ICastedObject LayerMask(int mask);
        public ICastedObject TriggerInteraction(QueryTriggerInteraction triggerInteraction);
        public ICastedObject IgnoreTrigger(bool ignore = true);
        public bool Cast(out RaycastHit hitInfo);
        public RaycastHit[] CastAll();
    }
}