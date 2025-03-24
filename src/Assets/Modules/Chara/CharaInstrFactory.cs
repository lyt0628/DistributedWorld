using QS.Api.Chara.Service;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;

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

        public ICharaControlInstr CharaConrol(float horizontal, float vertical, bool dash, bool jump, bool crouch, bool block, Vector3 baseRight, Vector3 baseForword, Vector3 baseUp)
        {
            return new CharaControlInstr(horizontal, vertical, dash, jump, crouch, block, baseRight, baseForword, baseUp);
        }

        public ICharaControlInstr CharaControl(float horizontal, float vertical, bool dash, bool jump, bool crouch, bool block)
        {
            return CharaConrol(horizontal, vertical, dash, jump, crouch, block,
                        Vector3.right, Vector3.forward, Vector3.up);
        }



        IInstruction ICharaInsrFactory.ShuffleStep(Vector2 direction, Vector3 baseforword, Vector3 baseRight)
        {
            throw new System.NotImplementedException();
        }
    }

}