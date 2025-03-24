using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QS.Common.Util
{
    public static class AnimationUtil
    {
        public static void RebindAnimatorController(Animator animator, RuntimeAnimatorController controller)
        {
            animator.runtimeAnimatorController = controller;
            animator.Rebind();
        }

        public static void RebindSkeleton(Transform target, SkinnedMeshRenderer[] meshes, string skeletonName, Transform newSkeleton, Transform[] bones)
        {
            foreach (var mesh in meshes)
            {
                var unionBones = bones.Where(o => mesh.bones.Any(b => b.name == o.name));
                if (unionBones.Count() < mesh.bones.Count())
                {
                    Debug.LogError($"Not all original Meshes contained in new skeleton in target {target.name}");
                }
                mesh.bones = bones.Where(o => mesh.bones.Any(b => b.name == o.name)).ToArray();
            }
            GameObjectUtil.ReplaceChild(target, skeletonName, newSkeleton);
        }
        public static void RebindSkeleton(Transform target, SkinnedMeshRenderer mesh, string skeletonName, Transform newSkeleton, Transform[] bones)
        {
            var arr = new List<SkinnedMeshRenderer> { mesh }.ToArray();
            RebindSkeleton(target, arr, skeletonName, newSkeleton, bones);
        }
    }
}