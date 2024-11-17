

using QS.API;
using UnityEngine;

namespace QS
{
    class AttackedWhenInteract : MonoBehaviour, IInteractable, IAttackable
    {
        public IAttack Attack()
        {
            return new CAttack()
            {
                Atn = 120
            };
        }

        public void Interact()
        {
            Debug.Log(typeof(IPlayerManager).IsAssignableFrom(typeof(PlayerManager)));
           var playerManaer =  GameManager.Instance.GetManager<IPlayerManager>();
            if (playerManaer == null)
            {
                Debug.LogWarning("Player Manager is null");
                return;
            }

            var character = playerManaer.GetActivedCharacter();
            if (character != null)
            {
                var combator = character.GetComponent<CDefaultCombater>();
                if(combator != null)
                {
                    combator.Injured(Attack());
                }
            }
            else
            {
                Debug.LogError("activedCharacter does not exists!!!");
            }
        }
    }
}