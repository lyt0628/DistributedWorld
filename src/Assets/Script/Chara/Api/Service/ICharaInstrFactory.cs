using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.Chara.Domain.Instruction;

namespace QS.Api.Chara.Service
{
    public interface ICharaInsrFactory
    {
        IInstruction Injured(float atk, float matk);
        IInstruction Injured(IAttack attack);
    }
}