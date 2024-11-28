using QS.Domain.Combat;
using QS.Domain.Item;

namespace QS.Impl.Domain.Item
{
    /// <summary>
    /// The weapon that hold basic weapon function.
    /// </summary>
    internal class Weapon
        : Item, IWeapon, IWeaponAttribute_tag, IWeaponBreed_tag
    {

        public float Exp { get; set; }

        public IBuff MainBuff
        {
            get => ((IWeaponBreed)breed).MainBuff;
            set => ((WeaponBreed)breed).MainBuff = value;
        }

        public override Item Clone()
        {
            return new Weapon()
            {
                Breed = Breed,
                Exp = Exp
            };

        }

        public void Refine(float exp)
        {
            Exp += exp;
        }


    }
}