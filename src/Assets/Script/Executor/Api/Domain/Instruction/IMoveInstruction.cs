

using UnityEngine;

namespace QS.Api.Executor.Domain.Instruction
{
    /// <summary>
    /// Move with jumping in specific coordinate.
    /// </summary>
    public interface IMoveInstruction : IInstruction
    {
        /// <summary>
        /// Horizontal component of movement
        /// </summary>
        float Horizontal { get; }
        /// <summary>
        /// Vertaical component of movement
        /// </summary>
        float Vertical { get; }
        /// <summary>
        /// Jump
        /// </summary>
        bool Jump { get; }
        
        /// <summary>
        /// The right axis of coordinate where movement happens
        /// </summary>
        Vector3 BaseRight { get;}
        /// <summary>
        /// The forward axis of coordinate where movement happens
        /// </summary>
        Vector3 Baseforword { get;}
        /// <summary>
        /// The up axis of coordinate where movement happens
        /// </summary>
        Vector3 BaseUp { get; }

    }
}