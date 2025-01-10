using QS.Api.Combat.Domain;

namespace QS.WorldItem.Domain
{
    /// <summary>
    /// The flywight of <see cref="DefaultWeapon"/>.
    /// </summary>
    class DefaultWeaponBreed : BaseItemBreed, IWeaponBreed
    {
        public IBuff MainBuff { get; set; }
    }
}