


using QS.Api.Executor.Domain.Instruction;
using UnityEngine;

namespace QS.Executor.Domain.Instruction
{
    sealed class TranslateInstr : ITranslateInstr
    {
        public TranslateInstr(float horizontal, float vertical, bool dash, bool jump, Vector3 baseRight, Vector3 baseforword, Vector3 baseUp)
        {
            Horizontal = horizontal;
            Vertical = vertical;
            Jump = jump;
            BaseRight = baseRight;
            Baseforword = baseforword;
            BaseUp = baseUp;
            Dash = dash;
        }

        public float Horizontal { get; }
        public float Vertical { get; }
        public bool Jump { get; }
        public Vector3 BaseRight { get; }
        public Vector3 Baseforword { get; }
        public Vector3 BaseUp { get; }

        public bool Dash { get; }
    }
}