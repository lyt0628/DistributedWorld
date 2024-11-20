using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QS.API
{
    /// <summary>
    /// A PlayerControllerService knows response getting Player Input from Data Layer,
    /// which knows how to getting those data from User.
    /// 
    /// ������ ���� ��ҿ��������� ����ȡ ������ ��� λ����Ϣ.
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
