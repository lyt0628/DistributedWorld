using QS.Chara.Domain;
using System;
using UnityEngine.Events;

namespace QS.Player
{
    public class ActiveCharacterChangedEvent : UnityEvent<Character, Character> { }
    /// <summary>
    /// IPlayerData stores the infomation about character. 
    /// A Characer is a role in the world, that player used to play.
    /// e.g. How much Character that player can use, or now which character 
    /// plyaer is using.
    /// 
    /// ��ҵ�����ȫ������������ӿ��оͺ�
    /// </summary>
    public interface IPlayerData
    {
        Character ActivedCharacter { get; set; }
        ActiveCharacterChangedEvent CharacterChanged { get; }

        long CoinCount { get; }


    }
}