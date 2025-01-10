


using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Service;
using QS.Chara.Domain.Instruction;
using QS.Executor.Domain.Instruction;
using UnityEngine;

namespace QS.Executor.Service
{
    class InstructionFactory : IInstructionFactory
    {
        public IInstruction Injured(float atk, float matk)
        {
            return new InjuredInstr(atk, matk);
        }

        public IInstruction Injured(IAttack attack)
        {
            return new InjuredInstr(attack);
        }

        public IInstruction Instantiate(string prefab, Transform parent)
        {
            return Instantiate(prefab, parent, Vector3.zero, Quaternion.identity);
        }

        public IInstruction Instantiate(string prefab, Transform parent, Vector3 position, Quaternion rotation)
        {
            return new InstantiateInstruction(prefab, parent, position, rotation);
        }

     
    }
}