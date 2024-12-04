using QS.WorldItem.Domain;

namespace QS.Api.WorldItem.Domain
{
    /// <summary>
    /// Weapon is a item, that add buff to character
    /// </summary>
    public interface IWeapon : IItem, IWeaponBreed, IWeaponAttribute
    {
        void Refine(float exp);
    }

}