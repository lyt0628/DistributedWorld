using QS.Api.Combat.Domain;

namespace QS.WorldItem.Domain
{
    /// <summary>
    /// The flywight of <see cref="Weapon"/>.
    /// </summary>
    class WeaponBreed : ItemBreed, IWeaponBreed, IWeaponBreed_tag
    {
        public WeaponBreed() { }
        public IBuff MainBuff { get; set; }
    }
}