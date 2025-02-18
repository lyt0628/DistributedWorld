

using QS.Api.Executor.Domain;
using System;

namespace QS.Executor.Instr
{
    class AddBlockedInstrsInstr : IInstruction
    {
        public Type[] BlockedInstrs { get; }

        public AddBlockedInstrsInstr(Type[] blockedInstrs)
        {
            this.BlockedInstrs = blockedInstrs;
        }

    }
}