

using UnityEngine;

namespace QS.Common.Util.Mounter
{
    public interface IMounterFactory {
        IMounter MountGameObject(GameObject target);
        IMounter MountRigidObject(GameObject target, SkinnedMeshRenderer skinnedMesh);
        IMounter MountRigidObjectOverride(
            GameObject target, string skeletonName, GameObject newSkeleton,
            SkinnedMeshRenderer[] skinnedMeshs);

    }

}