using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;
using QS.Chara.Domain.Instruction;
using UnityEngine;

namespace QS.Api.Chara.Service
{
    public interface ICharaInsrFactory
    {

        ICharaControlInstr CharaConrol(float horizontal, float vertical, bool dash, bool jump, bool crouch, Vector3 baseRight, Vector3 baseForword, Vector3 baseUp);
        ICharaControlInstr CharaControl(float horizontal, float vertical, bool dash, bool jump, bool crouch);

        IInstruction ShuffleStep(Vector2 direction, Vector3 baseforword, Vector3 baseRight);
    }
}