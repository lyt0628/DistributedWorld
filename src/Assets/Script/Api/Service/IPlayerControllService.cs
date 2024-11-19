using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QS.API
{
    /// <summary>
    /// A PlayerControllerService knows response getting Player Input from Data Layer,
    /// which knows how to getting those data from User.
    /// 
    /// 玩家外观 调用 玩家控制器服务 来获取 计算后的 玩家 位移信息.
    /// 
    /// <seealso cref="QS.API.IPlayerLocationData"/>
    /// <seealso cref="QS.API.IPlayerInputData"/>
    /// </summary>
    public interface IPlayerControllService
    {
        ICharacterTranslationDTO GetTranslation();
        Quaternion GetRotation();
    }

}
