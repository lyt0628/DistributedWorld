using QS.Domain.Combat;

namespace QS.Domain.Item
{
    /// <summary>
    /// The flywight of <see cref="SimpleWeaon"/>.
    /// </summary>
    public class SimpleWeaponBread : ItemBreed, IWeaponBreed
    {
        public SimpleWeaponBread(IItem delegt) : base(delegt) { }
        public IBuff MainBuff { get; set; }
    }
}