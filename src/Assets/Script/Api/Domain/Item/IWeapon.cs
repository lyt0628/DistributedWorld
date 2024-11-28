

using QS.Api.Domain.Item;

namespace QS.Domain.Item
{
    /// <summary>
    /// Weapon is a item, that add buff to character
    /// </summary>
    public interface IWeapon : IItem, IWeaponBreed, IWeaponAttribute
    {
        void Refine(float exp);
    }

}