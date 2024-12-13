



using QS.Api.Combat.Domain;

namespace QS.Api.Character.Service
{ 
    public interface ICharacterBuilder
    {
        ICharacterBuilder Combatable(IBuffedCombater combater);
        ICharacterBuilder Uncombetable();
        ICharacterBuilder Controlable();
        ICharacterBuilder Uncontrolable();
        ICharacterBuilder Skilled();
        ICharacterBuilder Unskilled();
        ICharacter Build();

    }
}