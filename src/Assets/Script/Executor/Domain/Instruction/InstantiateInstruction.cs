



using UnityEngine;

namespace QS.Executor.Domain.Instruction
{
    sealed class InstantiateInstruction : IInstantiateInstruction
    {
        public InstantiateInstruction(string prefab, Transform parent, Vector3 position, Quaternion rotation) 
        { 
            Prefab = prefab;
            Parent = parent;
            Position = position;
            Rotation = rotation;
        }
        public string Prefab { get; }

        public Transform Parent { get; }

        public Vector3 Position { get; }

        public Quaternion Rotation { get; }
    }
}