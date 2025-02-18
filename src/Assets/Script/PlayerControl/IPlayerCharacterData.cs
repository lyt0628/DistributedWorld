using QS.Chara.Domain;
using System;
using UnityEngine.Events;

namespace QS.PlayerControl
{
    public class ActiveCharacterChangedEvent : UnityEvent<Character, Character> { }
    /// <summary>
    /// IPlayerCharacterData stores the infomation about character. 
    /// A Characer is a role in the world, that player used to play.
    /// e.g. How much Character that player can use, or now which character 
    /// plyaer is using.
    /// 
    /// ·þ„Õ
    /// </summary>
    public interface IPlayerCharacterData
    {
        Character ActivedCharacter { get; set; }
        ActiveCharacterChangedEvent CharacterChanged { get; }

    }
}