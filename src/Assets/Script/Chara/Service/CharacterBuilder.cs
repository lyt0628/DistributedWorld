


using GameLib.DI;
using QS.Api.Character;
using QS.Api.Character.Service;
using QS.Api.Combat.Domain;
using QS.Api.Executor.Service;
using QS.Chara.Domain;
using QS.GameLib.Util;
using UnityEngine;

namespace QS.Chara.Service
{
    class CharacterBuilder : ICharacterBuilder
    {

        [Injected]
        ICharaAblityFactory characterInstructionHandlerFactory;
        [Injected]
        IInstructionHandlerFactory instructionHandlerFactory;   

        GameObject gameObject;
        bool combatable = false;
        IBuffedCombater combater;

    

        public CharacterBuilder(GameObject gameObject) 
        {
            this.gameObject = gameObject;
        }

        public ICharacter Build()
        {
            if (gameObject.TryGetComponent<Character>(out var c))
            {
                GameObject.Destroy(c);
            }

            gameObject.AddComponent<Character>();
            var characterBehaviour = gameObject.GetComponent<Character>();
            if (combatable)
            {
                characterBehaviour.AddLast(MathUtil.UUID(), 
                                characterInstructionHandlerFactory.Injured(characterBehaviour, combater));
            }
            return characterBehaviour;
        }

        public ICharacterBuilder Combatable(IBuffedCombater combater)
        {
            this.combater = combater ?? throw new System.InvalidOperationException("Combator cannot be null");
            combatable = true;

            return this;
        }

        public ICharacterBuilder Controlable()
        {
            throw new System.NotImplementedException();
        }

        public ICharacterBuilder Skilled()
        {
            throw new System.NotImplementedException();
        }

        public ICharacterBuilder Uncombetable()
        {
            combatable = false;
            combater = null;
            return this;
        }

        public ICharacterBuilder Uncontrolable()
        {
            throw new System.NotImplementedException();
        }

        public ICharacterBuilder Unskilled()
        {
            throw new System.NotImplementedException();
        }
    }
}