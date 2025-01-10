using GameLib.DI;
using QS.Api.Control.Service.DTO;
using UnityEngine;

namespace QS.Impl.Service.DTO
{
    class CharaTranslation : ICharaTranslation
    {
        public float Speed { get; set; }

        public Vector3 Displacement { get; set; }

        public bool Jumping { get; set; }

    }
}
