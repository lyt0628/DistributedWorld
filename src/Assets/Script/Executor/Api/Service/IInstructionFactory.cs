


using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;
using UnityEngine;

namespace QS.Api.Executor.Service
{
    public interface IInstructionFactory
    {
        IInstruction Move(
            float horizontal, float vertical, bool jump,
            Vector3 baseRight, Vector3 baseForword, Vector3 baseUp);
        IInstruction Move(
            float horizontal, float vertical, bool jump);

        IInstruction Instantiate(string prefab, Transform parent);
        IInstruction Instantiate(string prefab, Transform parent, Vector3 position, Quaternion rotation);
    }
}