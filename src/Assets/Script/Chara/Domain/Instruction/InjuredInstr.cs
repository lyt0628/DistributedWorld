


using QS.Api.Character.Instruction;

namespace QS.Chara.Domain.Instruction
{
    class InjuredInstr : IInjuredInstruction
    {
        public InjuredInstr(float atk, float matk)
        {
            Atk = atk;
            Matk = matk;
        }

        public float Atk { get; }

        public float Matk { get; }
    }

}