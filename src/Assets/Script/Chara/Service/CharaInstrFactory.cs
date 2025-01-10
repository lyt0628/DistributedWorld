


using QS.Api.Chara.Service;
using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.Chara.Domain.Instruction;
using QS.Executor.Domain.Instruction;
using UnityEngine;

namespace QS.Chara.Service
{
    class CharaInstrFacotry : ICharaInsrFactory
    {
     
        public IInstruction Translate(float horizontal, float vertical, bool dash, bool jump, Vector3 baseRight, Vector3 baseForword, Vector3 baseUp)
        {
            return new TranslateInstr(horizontal, vertical, dash, jump, baseRight, baseForword, baseUp);
        }

        public IInstruction Translate(float horizontal, float vertical, bool dash, bool jump)
        {
            return Translate(horizontal, vertical, dash, jump,
                        Vector3.right, Vector3.forward, Vector3.up);
        }
    }

}