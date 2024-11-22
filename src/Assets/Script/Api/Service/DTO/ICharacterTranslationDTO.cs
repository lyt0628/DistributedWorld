

using GameLib.DI;
using UnityEngine;

namespace QS.API
{

    public interface ICharacterTranslationDTO {
        float Speed {  get; }
        Vector3 Displacement {  get; }
        bool Jumping { get; }

    }

}