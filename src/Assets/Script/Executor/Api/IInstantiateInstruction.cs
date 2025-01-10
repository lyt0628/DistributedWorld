



using QS.Api.Executor.Domain;
using UnityEngine;

namespace QS.Executor.Domain.Instruction
{
    /// <summary>
    /// Instantiate prefab relative to parent. 
    /// 
    /// </summary>
    public interface IInstantiateInstruction : IInstruction
    {
        /// <summary>
        /// Resource name of prefab
        /// </summary>
        string Prefab { get; }
        /// <summary>
        /// Parent GameObject of new instance
        /// </summary>
        Transform Parent { get; }
        /// <summary>
        /// local position relative to <see cref="Parent"/>
        /// </summary>
        Vector3 Position { get; }
        /// <summary>
        /// local rotation relative to <see cref="Parent"/>
        /// </summary>
        Quaternion Rotation { get; }

    }
}
