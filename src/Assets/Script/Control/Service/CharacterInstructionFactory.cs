


using QS.Api.Control;
using QS.Api.Control.Domain;
using QS.Control.Domain.Instruction;
using UnityEngine;

namespace QS.Control.Service
{
    class CharacterInstructionFactory 
    {
        public ICharacterInstruction Jump()
        {
            return new JumpInstruction();
        }

        public ICharacterInstruction Move(Vector2 t)
        {
            return new MoveInstruction(t);
        }
    }
}