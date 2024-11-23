


using GameLib.DI;
using QS.Api.Data;
using UnityEngine;

namespace GameLib.Impl
{

    public class PlayerLocationData : IPlayerLocationData
    {

        [Injected]
        readonly IPlayerCharacterData playerCharacter;

        public Vector3 Location => playerCharacter.ActivedCharacter.transform.position;

        public Quaternion Rotation => playerCharacter.ActivedCharacter.transform.rotation;

        public Vector3 Right
        {
            get
            {
                var r = Camera.main.transform.right;
                r.y = 0;
                return r.normalized;
            }
        }

        public Vector3 Forward
        {
            get
            {
                var f = Camera.main.transform.forward;
                f.y = 0;
                return f.normalized;
            }
        }
        public Vector3 Up => playerCharacter.ActivedCharacter.transform.up;

        public CapsuleCollider Collider
        {
            get
            {
                var collider = playerCharacter.ActivedCharacter.GetComponent<CapsuleCollider>();
                if (collider == null)
                {
                    throw new System.Exception("Can not get Collider from Player Character:" +
                                               playerCharacter.ActivedCharacter);
                }
                return collider;
            }
        }

    }
}