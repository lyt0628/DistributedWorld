




using QS.Domain.Item;

namespace QS.Api.Service
{
    /// <summary>
    /// IWeaponRefineService provides operations on Weapon that already 
    /// stored in Inventory
    /// <seealso cref="IPlayerInventoryService"/>
    /// </summary>
    public interface IWeaponRefineService
    {
        void Refine(IItem weapon, float exp);
    }
}