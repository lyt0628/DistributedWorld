


using QS.Api.Executor.Domain.Instruction;
using UnityEngine;

namespace QS.Executor.Domain.Instruction
{
    sealed class MoveInstruction : IMoveInstruction
    {
        public MoveInstruction(float horizontal, float vertical, bool jump, Vector3 baseRight, Vector3 baseforword, Vector3 baseUp)
        {
            Horizontal = horizontal;
            Vertical = vertical;
            Jump = jump;
            BaseRight = baseRight;
            Baseforword = baseforword;
            BaseUp = baseUp;
        }

        public float Horizontal { get; }
        public float Vertical { get; }
        public bool Jump { get; }
        public Vector3 BaseRight { get; }
        public Vector3 Baseforword { get; }
        public Vector3 BaseUp { get; }
    }
}