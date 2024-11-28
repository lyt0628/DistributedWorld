using QS.Domain.Item;
using QS.Impl.Data.Store;

namespace QS.Impl.Data.Gateway
{

    public class WorldWeaponRecord
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