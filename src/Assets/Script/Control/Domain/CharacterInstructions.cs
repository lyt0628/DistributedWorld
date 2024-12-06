


using QS.Api.Control.Domain;
using UnityEngine;

namespace QS.Control.Domain.Instruction
{
    class MoveInstruction : ICharacterInstruction
    {
        public Vector2 Value { get; set; }
        public MoveInstruction(Vector2 value)
        {
            Value = value;
        }
    }

    class JumpInstruction : ICharacterInstruction
    {
    }
}