using GameLib.DI;
using QS.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QS.Impl.Service.DTO
{
    [Scope(Value =ScopeFlag.Prototype)]
    public class CharacterTranslationDTO : ICharacterTranslationDTO
    {

        public float Speed { get; set; }

        public Vector3 Displacement { get; set; }

        public bool Jumping {  get; set; }
    }
}
