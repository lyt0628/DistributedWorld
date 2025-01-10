


using System;
using UnityEngine;
using UnityEngine.XR;

namespace QS.Common.Util.Mounter
{
    /// <summary>
    /// 
    /// </summary>
    class RigidObjectMounter : IMounter
    {
        readonly SkinnedMeshRenderer skinnedMesh;
        readonly ObjectMounter mounter;
     
        

        public RigidObjectMounter(GameObject target, SkinnedMeshRenderer skinnedMesh)
        {
            this.skinnedMesh = skinnedMesh;
            mounter = new ObjectMounter(target);
            
        }

        public void Mount(GameObject gameObject)
        {
            mounter.Mount(gameObject);
            if (gameObject.TryGetComponent<SkinnedMeshRenderer>(out var skin))
            {
                skin.bones = skinnedMesh.bones;
            }
            else
            {
                throw new ArgumentException($"GameObject {gameObject.name} has no SkinnedMeshRender Component");
            }
         }
    }
}