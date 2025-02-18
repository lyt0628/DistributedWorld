

using QS.Api.Executor.Domain;
using System;

namespace QS.Executor.Instr
{
    class RemoveBlockedInstrsInstr : IInstruction
    {
        public Type[] BlockedInstrs { get; }

        public RemoveBlockedInstrsInstr(Type[] blockedInstrs)
        {
            this.BlockedInstrs = blockedInstrs;
        }

    }
}