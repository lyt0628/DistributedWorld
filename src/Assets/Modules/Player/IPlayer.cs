using QS.Chara.Domain;
using UnityEngine.Events;

namespace QS.Player
{
    public class ActiveCharacterChangedEvent : UnityEvent<Character, Character> { }
    /// <summary>
    /// IPlayer stores the infomation about chara. 
    /// A Characer is a role in the world, that player used to play.
    /// e.g. How much Character that player can use, or now which chara 
    /// plyaer is using.
    /// 
    /// ��ҵ�����ȫ������������ӿ��оͺ�
    /// </summary>
    public interface IPlayer
    {
        Character ActiveChara { get; set; }
        ActiveCharacterChangedEvent CharacterChanged { get; }

        int CoinCount { get; }


    }
}