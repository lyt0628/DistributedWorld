using QS.Domain.Combat;
using QS.Domain.Item;

namespace QS.Impl.Domain.Item
{
    /// <summary>
    /// The flywight of <see cref="Weapon"/>.
    /// </summary>
    public class WeaponBreed : ItemBreed, IWeaponBreed, IWeaponBreed_tag
    {
        public WeaponBreed() { }
        public IBuff MainBuff { get; set; }
    }
}