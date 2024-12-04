using QS.Api.WorldItem.Domain;
using QS.Impl.Data.Store;
using QS.WorldItem.Domain;

namespace QS.WorldItem.Repo.ActiveRecord
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