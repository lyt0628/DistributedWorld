

namespace QS.Domain.Item
{
    /// <summary>
    /// Weapon is a item, that add buff to character
    /// </summary>
    public interface IWeapon : IItem, IWeaponBreed
    {
        float Exp { get; }
        void Refine(float exp);
    }

}