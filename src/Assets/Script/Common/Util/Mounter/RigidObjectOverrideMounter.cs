

using System;
using UnityEngine;

namespace QS.Common.Util.Mounter
{
    /// <summary>
    /// íÏ›dŒÔÛw£¨ÅK∏¸–¬π«˜¿
    /// </summary>
    class RigidObjectOverrideMounter : IMounter
    {
        readonly GameObject target;
        readonly string skeletonName;
        readonly GameObject newSkeleton;
        readonly SkinnedMeshRenderer[] skinnedMeshs;
        readonly ObjectMounter mounter;

        public RigidObjectOverrideMounter(GameObject target, string skeletonName, GameObject newSkeleton, SkinnedMeshRenderer[] skinnedMeshs)
        {
            this.target = target;
            this.skeletonName = skeletonName;
            this.newSkeleton = newSkeleton;
            this.skinnedMeshs = skinnedMeshs;
            mounter = new ObjectMounter(target);
        }   

        public void Mount(GameObject gameObject)
        {
            
            mounter.Mount(gameObject);
            if (gameObject.TryGetComponent<SkinnedMeshRenderer>(out var skin))
            {
                Transform[] newBones;
                if(skin.bones != null)
                {
                    newBones = skin.bones;
                }
                else
                {
                    newBones = GameObjectUtil.CollectChildren(newSkeleton.transform);
                }

                foreach (var meshs in skinnedMeshs)
                {
                    meshs.bones = newBones;
                }
                GameObjectUtil.ReplaceChild(target.transform, skeletonName, newSkeleton.transform);
            }
            else
            {
                throw new ArgumentException($"GameObject {gameObject.name} has no SkinnedMeshRender Component");
            }
        }
    }
}