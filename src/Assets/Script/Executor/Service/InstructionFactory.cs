


using QS.Api.Executor.Domain;
using QS.Api.Executor.Service;
using QS.Executor.Domain.Instruction;
using UnityEngine;

namespace QS.Executor.Service
{
    class InstructionFactory : IInstructionFactory
    {
        public IInstruction Instantiate(string prefab, Transform parent)
        {
            return Instantiate(prefab, parent, Vector3.zero, Quaternion.identity);
        }

        public IInstruction Instantiate(string prefab, Transform parent, Vector3 position, Quaternion rotation)
        {
            return new InstantiateInstruction(prefab, parent, position, rotation);
        }

        public IInstruction Move(float horizontal, float vertical, bool jump, Vector3 baseRight, Vector3 baseForword, Vector3 baseUp)
        {
            return new MoveInstruction( horizontal, vertical, jump, baseRight, baseForword, baseUp);
        }

        public IInstruction Move( float horizontal, float vertical, bool jump)
        {
            var baseRight = new Vector3
            {
                x = 1
            };
            var baseForword = new Vector3
            {
                z = 1
            };
            var baseUp = new Vector3
            {
                y = 1
            };
            return Move( horizontal, vertical, jump, baseRight, baseForword, baseUp);
        }
    }
}