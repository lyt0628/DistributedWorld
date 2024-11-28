

using QS.Domain.Combat;

namespace QS.Domain.Item
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