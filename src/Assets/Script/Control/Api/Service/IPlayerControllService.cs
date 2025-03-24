using QS.Api.Service.DTO;
using QS.GameLib.Rx.Relay;
using UnityEngine;


namespace QS.Api.Service
{
    /// <summary>
    /// ��ҿ��ƶ���ͽ������ָ��(��UI),���󲿷ֵ���ҿ���,����,
    /// �������ƶ�, ս����������, �ڴ�֮ǰ����Ҫ�� Unity���� �� ��ʤ������һЩ�о��ͷ�װ
    /// �����ṩһЩ����ע��(TODO)
    /// A PlayerControllService takes responsibility for computing the controll information of player.
    /// <seealso cref="Domain.IPlayerLocationData"/>
    /// <seealso cref="Domain.IPlayerInputData"/>
    /// </summary>
    public interface IPlayerControllService
    {
        ICharacterTranslation GetTranslation();

        Relay<ICharacterTranslation> GetPlayerTranslation();

        Quaternion GetRotation();
    }

}
