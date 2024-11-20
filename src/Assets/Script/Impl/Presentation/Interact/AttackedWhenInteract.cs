

using GameLib.DI;
using QS.API;
using QS.API.Data;
using UnityEngine;

namespace QS
{
    class AttackedWhenInteract : MonoBehaviour, IInteractable, IAttackable
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
            return new CAttack()
            {
                Atn = 120
            };
        }

        public void Interact()
        {

            var character = PlayerCharacter.ActivedCharacter;
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