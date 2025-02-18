


using QS.Api.Chara.Service;
using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;
using QS.Chara.Domain.Instruction;
using QS.Chara.Instrs;
using QS.Executor.Domain.Instruction;
using UnityEngine;

namespace QS.Chara.Service
{
    class CharaInstrFacotry : ICharaInsrFactory
    {
        public IInstruction ShuffleStep(Vector2 direction, Vector3 baseforword, Vector3 baseRight)
        {
            return new ShuffleStepInstr(direction, baseforword, baseRight);
        }

        public ICharaControlInstr CharaConrol(float horizontal, float vertical, bool dash, bool jump,bool crouch, Vector3 baseRight, Vector3 baseForword, Vector3 baseUp)
        {
            return new CharaControlInstr(horizontal, vertical, dash, jump, crouch, baseRight, baseForword, baseUp);
        }

        public ICharaControlInstr CharaControl(float horizontal, float vertical, bool dash, bool jump, bool crouch)
        {
            return CharaConrol(horizontal, vertical, dash, jump, crouch,
                        Vector3.right, Vector3.forward, Vector3.up);
        }
    }

}