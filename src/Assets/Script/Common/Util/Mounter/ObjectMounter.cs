



using UnityEngine;

namespace QS.Common.Util.Mounter
{
    /// <summary>
    /// �ь�����d��Ŀ���ϣ��������g��
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