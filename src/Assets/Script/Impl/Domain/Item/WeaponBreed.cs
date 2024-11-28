using QS.Domain.Combat;

namespace QS.Domain.Item
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