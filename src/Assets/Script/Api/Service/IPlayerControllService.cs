using QS.Api.Service.DTO;
using UnityEngine;


namespace QS.Api.Service
{
    /// <summary>
    /// A PlayerControllService takes responsibility for computing the controll information of player.
    /// <seealso cref="Domain.IPlayerLocationData"/>
    /// <seealso cref="Domain.IPlayerInputData"/>
    /// </summary>
    public interface IPlayerControllService
    {
        ICharacterTranslationDTO GetTranslation();
        Quaternion GetRotation();
    }

}
