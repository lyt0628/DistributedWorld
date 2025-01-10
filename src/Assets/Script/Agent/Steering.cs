


using GameLib.DI;
using QS.Api.Chara.Service;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;
using QS.Api.Executor.Service;
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

        public IInstruction GetTranslateInstr()
        {
            return instructionFactory.Translate( 1, 0, true, false, Vector3.right, Vector3.forward, Vector3.up);
        }
    }
}