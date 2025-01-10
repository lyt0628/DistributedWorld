

using QS.Api.WorldItem.Domain;
using QS.WorldItem.Domain;

namespace QS.Api.WorldItem
{
    public interface IItemBreedRepo
    {
        IWeaponBreed GetWeaponBreed(string name);
    }
}