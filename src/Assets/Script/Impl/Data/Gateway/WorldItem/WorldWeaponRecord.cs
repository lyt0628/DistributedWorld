using QS.Domain.Item;
using QS.Impl.Data.Store;
using QS.Impl.Domain.Item;

namespace QS.Impl.Data.Gateway.WorldItem
{

    internal class WorldWeaponRecord
        : WorldItemRecord<Weapon>,
        IWeaponAttribute, IWeaponAttribute_tag
    {
        public WorldWeaponRecord(ItemSotre sotre, Weapon target)
            : base(sotre, target)
        {
        }

        public float Exp
        {
            get => target.Exp;
            set => target.Exp = value;
        }

    }
}