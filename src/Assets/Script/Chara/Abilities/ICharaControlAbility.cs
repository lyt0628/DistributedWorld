

using QS.Api.Executor.Domain;

namespace QS.Chara.Abilities
{
    public interface ICharaConrolAbility : IInstructionHandler
    {
        bool Enabled { get; set;}
        CharaControlState State { get; }
        //bool Grounded { get; }
        //bool Crouching { get; }

    }
}