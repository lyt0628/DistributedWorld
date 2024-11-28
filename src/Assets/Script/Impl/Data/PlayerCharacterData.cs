using QS.Api.Data;
using System;
using UnityEngine;

namespace QS.Impl.Data
{
    public class PlayerCharacterData : IPlayerCharacterData
    {

        private GameObject activedCharacter;
        private event Action ActivatedCharacterChangedCallback = () => { };
        public GameObject ActivedCharacter
        {
            get { return activedCharacter; }
            set
            {
                activedCharacter = value;
                ActivatedCharacterChangedCallback();
            }
        }

        public void AddListenerForActivatedCharacterChanged(Action callback)
        {
            ActivatedCharacterChangedCallback += callback;
        }

        public void RemoveListenerForActivatedCharacterChanged(Action callback)
        {
            ActivatedCharacterChangedCallback -= callback;
        }
    }
}