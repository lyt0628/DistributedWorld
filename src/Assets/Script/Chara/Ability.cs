


using QS.Api.Executor.Domain;
using QS.Executor.Domain;

namespace QS.Chara.Domain
{
    /// <summary>
    /// I am soryy change the name from 'Handler' to 'Abilty', but we does need a shorter and doamin
    /// specific name for this class.
    /// </summary>
    public abstract class Ability : AbstractHandler
    {
        protected Ability(Character character) : base(character)
        {
            Character = character;
        }

        public Character Character { get; }
    }
}
