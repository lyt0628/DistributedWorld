using QS.Api.Combat.Domain;

namespace QS.WorldItem.Domain
{
    /// <summary>
    /// The weapon Defferent fields with item.
    /// and used as a flyWeight definition 
    /// </summary>
    public interface IWeaponBreed
    {
        IBuff MainBuff { get; }
    }
}