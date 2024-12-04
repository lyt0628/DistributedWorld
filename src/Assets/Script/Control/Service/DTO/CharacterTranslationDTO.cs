using GameLib.DI;
using QS.Api.Service.DTO;
using UnityEngine;

namespace QS.Impl.Service.DTO
{
    class CharacterTranslationDTO : ICharacterTranslation
    {
        public float Speed { get; set; }

        public Vector3 Displacement { get; set; }

        public bool Jumping { get; set; }
    }
}
