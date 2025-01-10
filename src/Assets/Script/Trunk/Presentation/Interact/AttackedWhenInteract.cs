

using GameLib.DI;
using QS.Api.Chara.Service;
using QS.Api.Combat.Domain;
using QS.Api.Combat.Service;
using QS.Api.Data;
using QS.Api.Executor.Service;
using QS.Api.Presentation.Interact;
using QS.Chara.Domain;
using QS.Combat.Domain;
using UnityEngine;

namespace QS
{
    public class AttackedWhenInteract : MonoBehaviour, IInteractable
    {
        [Injected]
        readonly IPlayerCharacterData playerCharacter;

        [Injected]
        readonly IInstructionFactory instructionFactory;

        void Start()
        {
            TrunkGlobal.Instance.DI.Inject(this);
        }


        public void Interact()
        {
            var character = playerCharacter.ActivedCharacter;
            if (character != null)
            {
                var c= character.GetComponent<Character>();
                c.Execute(instructionFactory.Injured(120, 0));
            }
            else
            {
                Debug.LogError("activedCharacter does not exists!!!");
            }
            
        }
    }
}