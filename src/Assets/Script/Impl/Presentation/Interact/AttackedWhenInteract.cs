

using GameLib.DI;
using QS.Api.Data;
using QS.Api.Presentation.Interact;
using QS.Domain.Combat;
using UnityEngine;

namespace QS
{
    public class AttackedWhenInteract : MonoBehaviour, IInteractable, IAttackable
    {
        [Injected]
        IPlayerCharacterData PlayerCharacter { get; set; }

        void Start()
        {
            var ctx = GameManager.Instance.GlobalDIContext;
            ctx.Inject(this);
        }


        public IAttack Attack()
        {
            return new Attack()
            {
                Atn = 120
            };
        }

        public void Interact()
        {
            var character = PlayerCharacter.ActivedCharacter;
            if (character != null)
            {
                var combator = character.GetComponent<CombatorBehaviour>();
                combator?.Injured(Attack());
            }
            else
            {
                Debug.LogError("activedCharacter does not exists!!!");
            }
        }
    }
}