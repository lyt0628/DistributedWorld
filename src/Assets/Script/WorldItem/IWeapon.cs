using QS.WorldItem.Domain;

namespace QS.Api.WorldItem.Domain
{
    /// <summary>
    /// DefaultWeapon is a item, that add buff to character
    /// </summary>
    public interface IWeapon : IItem, IWeaponBreed
    {
        float Exp { get; }
        void Refine(float exp);
    }

}