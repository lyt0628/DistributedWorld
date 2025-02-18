


using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;
using QS.Executor.Instr;
using System;
using UnityEngine;

namespace QS.Api.Executor.Service
{
    public interface IInstructionFactory
    {
        IInstruction AddBlockedInstrs(Type[] instrs)
        {
            return new AddBlockedInstrsInstr(instrs);
        }
        IInstruction RemoveBlockedInstrs(Type[] instrs)
        {
            return new RemoveBlockedInstrsInstr(instrs);
        }
        IInstruction Injured(float atk, float matk);
        IInstruction Injured(IAttack attack);
        IInstruction Instantiate(string prefab, Transform parent);
        IInstruction Instantiate(string prefab, Transform parent, Vector3 position, Quaternion rotation);
    }
}