using UnityEngine;

namespace QS.Api.Control.Service.DTO
{

    /// <summary>
    /// Data Transfer Object for Character Translation.
    /// <seealso cref="IPlayerControllService"/>
    /// </summary>
    public interface ICharacterTranslationDTO
    {
        float Speed { get; }
        Quaternion Rotation { get; }
        Vector3 Displacement { get; }
        bool Jumping { get; }

    }

}