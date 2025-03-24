


using QS.Api.Executor.Domain;

namespace QS.Combat
{
    public interface ICombatInstrFacotry
    {
        IInstruction AttackedInstr(CombatorBehaviour attacker, ISkill skill);

    }


    class CombatInstrFactory : ICombatInstrFacotry
    {
        public IInstruction AttackedInstr(CombatorBehaviour attacker, ISkill skill)
        {
            return new AttackedInstr(attacker, skill);
        }
    }
}