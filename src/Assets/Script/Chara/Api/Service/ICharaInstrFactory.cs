using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.Chara.Domain.Instruction;
using UnityEngine;

namespace QS.Api.Chara.Service
{
    public interface ICharaInsrFactory
    {

        IInstruction Translate(
        float horizontal, float vertical, bool dash, bool jump,
        Vector3 baseRight, Vector3 baseForword, Vector3 baseUp);
        IInstruction Translate(
            float horizontal, float vertical, bool dash, bool jump);
    }
}