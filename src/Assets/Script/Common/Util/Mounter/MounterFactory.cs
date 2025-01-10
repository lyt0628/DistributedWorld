

using UnityEngine;

namespace QS.Common.Util.Mounter
{
    class MounterFactory : IMounterFactory
    {
        public IMounter MountGameObject(GameObject target)
        {
            return new ObjectMounter(target);
        }

        public IMounter MountRigidObject(GameObject target, SkinnedMeshRenderer skinnedMesh)
        {
            return new RigidObjectMounter(target, skinnedMesh);
        }

        public IMounter MountRigidObjectOverride(
            GameObject target, string skeletonName, GameObject newSkeleton, 
            SkinnedMeshRenderer[] skinnedMeshs)
        {
            return new RigidObjectOverrideMounter(target, skeletonName, newSkeleton,skinnedMeshs);
        }
    }
}