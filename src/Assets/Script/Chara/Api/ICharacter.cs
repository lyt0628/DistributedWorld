using QS.Api.Common;
using QS.Api.Executor.Domain;
using QS.GameLib.Pattern.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace QS.Api.Character
{
    /// <summary>
    /// ��ɫ����Ϸ�еĺ��Ķ���, ����ʾ���������ʵ��,
    /// ʵ������Executor �е�, Executor ���漰����, �������������չ
    /// ����רע���������� ��ɫ��ʹ�ü���,�����ܱ������ɫ�����
    /// ����ֻ��һ���ر��Handler�ܴ���һЩ����ָ��
    /// </summary>
    public interface ICharacter  : IRelayExecutor,  IMessagerProvider, IAnimAware
    {

    }

}
