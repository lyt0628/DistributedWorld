


using QS.Api.Chara.Service;
using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.Chara.Domain.Instruction;

namespace QS.Chara.Service
{
    class CharaInstrFacotry : ICharaInsrFactory
    {
        public IInstruction Injured(float atk, float matk)
        {
            return new InjuredInstr(atk, matk);
        }

        public IInstruction Injured(IAttack attack)
        {
            return new InjuredInstr(attack);
        }
    }

}