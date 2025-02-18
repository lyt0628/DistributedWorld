


using QS.Api.Chara.Instruction;
using QS.Api.Combat.Domain;
using QS.Api.Combat.Service;
using QS.Api.Executor.Domain;
using QS.Executor;

namespace QS.Chara.Domain.Instruction
{
    class InjuredInstr : IInjuredInstr
    {
        public InjuredInstr(float atk, float matk)
        {
            var atkFactory = ExecutorGlobal.Instance.GetInstance<IAttackFactory>();
            Attack = atkFactory.NewAttack(atk, matk);
        }
        public InjuredInstr(IAttack attack) 
        {
            Attack = attack;
        }

        public IAttack Attack { get; }
    }

}