


using QS.Api.Executor.Domain.Instruction;
using UnityEngine;

namespace QS.Executor.Domain.Instruction
{
    sealed class CharaControlInstr : ICharaControlInstr
    {
        public CharaControlInstr(float horizontal, float vertical, bool dash, bool jump, bool crouch, Vector3 baseRight, Vector3 baseforword, Vector3 baseUp)
        {
            Horizontal = horizontal;
            Vertical = vertical;
            Jump = jump;
            this.Crouch = crouch;
            BaseRight = baseRight;
            Baseforword = baseforword;
            BaseUp = baseUp;
            Dash = dash;
        }

        public float Horizontal { get; }
        public float Vertical { get; }
        public bool Dash { get; }
        public bool Jump { get; }
        public bool Crouch { get; }
        public Vector3 BaseRight { get; }
        public Vector3 Baseforword { get; }
        public Vector3 BaseUp { get; }


    }
}