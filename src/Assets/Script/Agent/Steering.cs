


using GameLib.DI;
using QS.Api.Chara.Service;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;
using QS.Api.Executor.Service;
using QS.PlayerControl;
using UnityEngine;

namespace QS.Agent
{
    /// <summary>
    /// �Ƅ�ָʾ���@ȡ��һ�����Ƅ��ٶ�,
    /// ���ҵ��������У��@ȡ�đ�ԓ���Ƅ�ָ��
    /// </summary>
    class Steering : ISteering
    {
        [Injected]
        readonly ICharaInsrFactory instructionFactory;
        [Injected]
        readonly IPlayerCharacterData playerChara;

        public Transform robot;

        public ICharaControlInstr GetTranslateInstr()
        {
            var direction = playerChara.ActivedCharacter.transform.position - robot.position;
            if (direction.magnitude < 1) {
                direction = Vector3.zero;
            }
            else
            {
                direction = direction.normalized * 0.9f;
            }

            return instructionFactory.CharaControl(direction.x, direction.z, true, false, false);
        }
    }
}