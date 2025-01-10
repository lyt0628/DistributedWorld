


using QS.Api.Control.Domain;
using UnityEngine;

namespace QS.Control.Domain
{
    /// <summary>
    /// Entity
    /// </summary>
    interface ICharaTranslationProxy : ICharaTranslationSnapshot
    {
        string UUID { get; }  

        /// <summary>
        /// The Speed of proxy object in vertical axis.
        /// </summary>
        public float VerticalSpeed { get; set; }
        
        /// <summary>
        /// Inner ResourceStatus indicates if character is jumping.
        /// </summary>
        public bool Jumping { get; set; }

        public void UpdateSnapshot(ICharaTranslationSnapshot snapshot);
    }
}