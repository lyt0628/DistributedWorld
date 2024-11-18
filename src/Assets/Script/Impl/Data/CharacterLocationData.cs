


using GameLib.DI;
using QS.API;
using UnityEngine;

namespace GameLib.Impl
{
    class CharacterLocationData : ICharacterLocationData
    {

        [Injected(Name ="Player")]
        GameObject player;
        [Injected(Name ="PlayerTransform")]
        Transform Transform { get; set; }

        [Injected(Name ="MainCamera")]
        Camera MainCamera { get; set; }

        public Vector3 Location => Transform.position;

        public Quaternion Rotation => Transform.rotation;

        public Vector3 Right => MainCamera.transform.right;

        public Vector3 Forward => MainCamera.transform.forward;
        public Vector3 Up => player.transform.up;

        public CapsuleCollider Collider { get
            {
                var collider = player.GetComponent<CapsuleCollider>();
                if(collider == null)
                {
                    throw new System.Exception("Can not get Collider from Player Character:" + player);
                }
                return collider;
            } }

    }
}