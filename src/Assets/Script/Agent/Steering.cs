


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
    /// 移動指示，獲取下一步的移動速度,
    /// 在我的上下文中，獲取的應該是移動指令
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