using UnityEngine;

namespace QS.Api.Data
{


    /// <summary>
    /// PlayerLocationData is a datasource to provide information 
    /// about the location of character that player is used now.
    /// All Domain in this Interface is the pure-function like, 
    /// having nothing side-effect.
    /// </summary>
    public interface IPlayerLocationData
    {
        Vector3 Location { get; }
        Quaternion Rotation { get; }

        Vector3 Right { get; }
        Vector3 Forward { get; }

        Vector3 Up { get; }
        CapsuleCollider Collider { get; }


    }
}
