



using UnityEngine;

namespace QS.Common.Util.Mounter
{
    /// <summary>
    /// 把對象掛載到目標上，帶有中間層
    /// </summary>
    class ObjectMounter : IMounter
    {
        readonly GameObject target;
        public ObjectMounter(GameObject target)
        {
            this.target = target;
        }

        public void Mount(GameObject gameObject)
        {
            gameObject.transform.parent = target.transform;
        }
    }
}