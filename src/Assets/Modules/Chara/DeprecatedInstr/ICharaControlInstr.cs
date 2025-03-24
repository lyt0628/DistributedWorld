using UnityEngine;

namespace QS.Api.Executor.Domain.Instruction
{
    /// <summary>
    /// CharaControl with inAir in specific coordinate.
    /// </summary>
    public interface ICharaControlInstr : IInstruction
    {
        /// <summary>
        /// horizontal component of movement
        /// </summary>
        float Horizontal { get; }
        /// <summary>
        /// Vertaical component of movement
        /// </summary>
        float Vertical { get; }

        bool Run { get; }
        /// <summary>
        /// jump
        /// </summary>
        bool Jump { get; }

        bool ToggleFocus { get; }

        bool Block { get; }

        bool ToggleCrounch { get; }

        /// <summary>
        /// The right axis of coordinate where movement happens
        /// </summary>
        Vector3 BaseRight { get; }
        /// <summary>
        /// The forward axis of coordinate where movement happens
        /// </summary>
        Vector3 Baseforword { get; }
        /// <summary>
        /// The up axis of coordinate where movement happens
        /// </summary>
        Vector3 BaseUp { get; }

    }
}