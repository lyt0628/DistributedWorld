

using QS.Api.Executor.Domain;
using UnityEngine;

namespace QS.Chara
{
    public interface IAimLockInstr : IInstruction
    {
        Transform Target { get; set; }
    }

    struct AimLockInstr : IAimLockInstr
    {
        public Transform Target { get; set; }
    }
}