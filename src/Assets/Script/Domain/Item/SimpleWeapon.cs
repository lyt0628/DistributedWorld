using GameLib.Util;
using QS.Domain.Combat;

namespace QS.Domain.Item
{
    /// <summary>
    /// The weapon that hold basic weapon function.
    /// </summary>
    public class SimpleWeaon : AbstractItem, IWeapon
    {
        private readonly SimpleWeaponBread bread;
        public float Exp { get; set; }
        public SimpleWeaon(string uuid, SimpleWeaponBread itemBreed)
            : base(uuid, itemBreed)
        {
            this.bread = itemBreed;
        }

        public IBuff MainBuff => bread.MainBuff;

        public override IItem Clone()
        {
            var weapon = new SimpleWeaon(MathUtil.UUID(), bread)
            {
                Exp = Exp
            };
            return weapon;
        }

        public void Refine(float exp)
        {
            Exp += exp;
        }


    }
}