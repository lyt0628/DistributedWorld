

using GameLib.DI;
using QS.Api.Combat.Domain;
using QS.Api.Combat.Service;
using QS.Api.Data;
using QS.Api.Presentation.Interact;
using QS.Combat.Domain;
using UnityEngine;

namespace QS
{
    public class AttackedWhenInteract : MonoBehaviour, IInteractable, IAttackable
    {
        [Injected]
        IPlayerCharacterData PlayerCharacter { get; set; }

        [Injected]
        IAttackFactory AttackFactory { get; set; }

        void Start()
        {
            var ctx = TrunkGlobal.Instance.DI;
            ctx.Inject(this);
        }


        public IAttack Attack()
        {
            return AttackFactory.NewAttack(120, 0);
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