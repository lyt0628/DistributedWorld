using QS.Chara.Domain;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace QS.PlayerControl
{
    class PlayerCharacterData : IPlayerCharacterData
    {

        private Character activedCharacter;
        public ActiveCharacterChangedEvent CharacterChanged { get; } = new();

        public Character ActivedCharacter
        {
            get { return activedCharacter; }
            set
            {
                var oldChara = activedCharacter;
                activedCharacter = value;
                CharacterChanged.Invoke(value, oldChara);
                
            }
        }

       
    }
}