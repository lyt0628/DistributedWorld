


using GameLib.DI;
using QS.Api.Chara.Service;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;
using QS.Api.Executor.Service;
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

        public IInstruction GetTranslateInstr()
        {
            return instructionFactory.Translate( 1, 0, true, false, Vector3.right, Vector3.forward, Vector3.up);
        }
    }
}