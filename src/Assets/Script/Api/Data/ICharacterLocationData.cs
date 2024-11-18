using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QS.API
{
    public interface ICharacterLocationData 
    {
        Vector3 Location { get; }
        Quaternion Rotation { get; }

        Vector3 Right { get; }
        Vector3 Forward { get; }

        Vector3 Up { get; }
        CapsuleCollider Collider { get; }

    }
}
