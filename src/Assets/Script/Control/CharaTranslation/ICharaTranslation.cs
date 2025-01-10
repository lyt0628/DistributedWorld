using UnityEngine;

namespace QS.Api.Control.Service.DTO
{

    /// <summary>
    /// This value object defines what data the client needed.
    /// <seealso cref="IPlayerControllService"/>
    /// </summary>
    public interface ICharaTranslation
    {
        float Speed { get; }
        Vector3 Displacement { get; }
        bool Jumping { get; }

    }

}