using QS.Chara.Domain;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace QS.Player
{
    class PlayerData : IPlayerData
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

        public long CoinCount { get; set; }
    }
}