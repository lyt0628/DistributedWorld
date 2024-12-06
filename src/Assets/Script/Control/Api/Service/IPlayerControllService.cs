using QS.Api.Control.Service.DTO;
using QS.GameLib.Rx.Relay;
using UnityEngine;


namespace QS.Api.Service
{
    /// <summary>
    /// 玩家控制定义和接受玩家指令(非UI),做大部分的玩家控制,比如,
    /// 基本的移动, 战斗动作控制, 在此之前我需要对 Unity动画 和 决胜动作做一些研究和封装
    /// 对外提供一些控制注册(TODO)
    /// A PlayerControllService takes responsibility for computing the controll information of player.
    /// <seealso cref="Domain.IPlayerLocationData"/>
    /// <seealso cref="Domain.IPlayerInputData"/>
    /// </summary>
    public interface IPlayerControllService
    {
        //ICharacterTranslationDTO GetTranslation();

        Relay<ICharacterTranslationDTO> GetTranslation();
        Relay<Quaternion> GetRotation();




    }

}
