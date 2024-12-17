


using QS.Api.Chara.Instruction;
using QS.Api.Combat.Domain;
using QS.Api.Combat.Service;

namespace QS.Chara.Domain.Instruction
{
    class InjuredInstr : IInjuredInstruction
    {
        public InjuredInstr(float atk, float matk)
        {
            var atkFactory = CharaGlobal.Instance.GetInstance<IAttackFactory>();
            Attack = atkFactory.NewAttack(atk, matk);
        }
        public InjuredInstr(IAttack attack) 
        {
            Attack = attack;
        }

        public IAttack Attack { get; }
    }

}