


using QS.Api.Control.Domain;
using UnityEngine;

namespace QS.Control.Domain
{
    class CharaTranslationSnapshot : ICharaTranslationSnapshot
    {

        public Vector3 Position { get; set; }
        public float Horizontal { get; set; }
        public float Vertical { get; set; }
        public bool Jump { get; set; }
        public bool Dash { get; set; }
        public Vector3 BaseRight { get; set; }
        public Vector3 BaseForword { get; set; }
        public Vector3 BaseUp { get; set; }

        public Quaternion Rotation { get; set; }
      
    }
}