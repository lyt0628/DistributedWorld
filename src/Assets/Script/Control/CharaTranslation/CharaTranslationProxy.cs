

using QS.Api.Control.Domain;
using UnityEngine;

namespace QS.Control.Domain
{
    class CharaTranslationProxy : ICharaTranslationProxy
    {
        public CharaTranslationProxy(string uuid) 
        { 
            UUID = uuid;
        }
        ICharaTranslationSnapshot snapshot ;

        public void UpdateSnapshot(ICharaTranslationSnapshot snapshot)
        {
            this.snapshot = snapshot;
        }

        public string UUID { get; }
       
        public float VerticalSpeed { get; set; } = 0f;
        public bool Jumping { get; set; } = false;
        public Vector3 Position { get => snapshot.Position; set => snapshot.Position = value; }
        public Quaternion Rotation { get => snapshot.Rotation; set => snapshot.Rotation = value; }
        public float Horizontal { get => snapshot.Horizontal; set => snapshot.Horizontal = value; }
        public float Vertical { get => snapshot.Vertical; set => snapshot.Vertical = value; }
        public bool Dash { get => snapshot.Dash; set => snapshot.Dash = value; }
        public bool Jump { get => snapshot.Jump; set => snapshot.Jump = value; }
        public Vector3 BaseRight { get => snapshot.BaseRight; set => snapshot.BaseRight = value; }
        public Vector3 BaseForword { get => snapshot.BaseForword; set => snapshot.BaseForword = value; }
        public Vector3 BaseUp { get => snapshot.BaseUp; set => snapshot.BaseUp = value; }

      
    }
}