


using QS.API.Data;
using System;
using UnityEngine;

namespace QS.Impl.Data
{
    class PlayerCharacterData : IPlayerCharacterData
    {
        public PlayerCharacterData()
        {
            Debug.LogWarning("Reenter");
        }

        private GameObject activedCharacter;
        private event Action activatedCharacterChangedCallback = ()=>{};
        public GameObject ActivedCharacter 
        { 
            get { return activedCharacter; }
            set 
            { 
                activedCharacter = value;
                activatedCharacterChangedCallback();
            }
        }

        public void AddListenerForActivatedCharacterChanged(Action callback)
        {
            activatedCharacterChangedCallback += callback;
        }

        public void RemoveListenerForActivatedCharacterChanged(Action callback)
        {
            activatedCharacterChangedCallback -= callback;
        }
    }
}