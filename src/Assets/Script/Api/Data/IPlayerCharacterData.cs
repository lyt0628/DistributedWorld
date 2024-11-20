


using System;
using UnityEngine;

namespace QS.API.Data
{

    /// <summary>
    /// IPlayerCharacterData stores the infomation about character. 
    /// A Characer is a role in the world, that player used to play.
    /// e.g. How much Character that player can use, or now which character 
    /// plyaer is using.
    /// </summary>
    interface IPlayerCharacterData
    {
        GameObject ActivedCharacter { get; set; }
        void AddListenerForActivatedCharacterChanged(Action callback);
        void RemoveListenerForActivatedCharacterChanged(Action callback);

    }
}