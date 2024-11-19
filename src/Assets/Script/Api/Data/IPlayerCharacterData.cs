


using System;
using UnityEngine;

namespace QS.API.Data
{
    interface IPlayerCharacterData
    {
        GameObject ActivedCharacter { get; set; }
        void AddListenerForActivatedCharacterChanged(Action callback);
        void RemoveListenerForActivatedCharacterChanged(Action callback);

    }
}