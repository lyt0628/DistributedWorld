using QS.Api.WorldItem.Domain;
using QS.WorldItem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QS.WorldItem.Repo.ActiveRecord
{
    internal class WorldWeaponRepo
        : WorldItemRepo<Weapon, WorldWeaponRecord>
    {
        public override WorldWeaponRecord Create()
        {
            var w = new Weapon();
            var wr = new WorldWeaponRecord(store, w);
            return wr;
        }

        public override void DestroyAll(Predicate<Weapon> condition)
        {
            throw new NotImplementedException();
        }

        public override WorldWeaponRecord Find(Predicate<Weapon> condition)
        {
            return store.GetAll(ItemType.Weapon)
                .Select(i => i as Weapon)
                .Where(w => condition(w))
                .Select(w => new WorldWeaponRecord(store, w))
                .First();
        }

        public override IList<WorldWeaponRecord> Order(IComparer<Weapon> comparer)
        {
            throw new NotImplementedException();
        }

        public override ICollection<WorldWeaponRecord> Where(Predicate<Weapon> condition)
        {
            throw new NotImplementedException();
        }
    }
}
