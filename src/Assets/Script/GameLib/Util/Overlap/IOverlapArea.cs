

using UnityEngine;

namespace QS.GameLib.Util.Overlap
{
    public interface IOverlapArea
    {
        IOverlapArea LayerMask(int mask);
        IOverlapArea TriggerInteraction(QueryTriggerInteraction triggerInteraction);
        IOverlapArea IgnoreTrigger(bool ignore = true);

        Collider[] Overlap();
    }
}