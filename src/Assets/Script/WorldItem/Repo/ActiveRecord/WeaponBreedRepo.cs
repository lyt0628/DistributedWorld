using QS.Api.WorldItem.Domain;
using QS.WorldItem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QS.WorldItem.Repo.ActiveRecord
{
    public class WeaponBreedRepo
        : ItemBreedRepo<WeaponBreed, WeaponBreedRecord>
    {
        public override WeaponBreedRecord Create()
        {
            var wb = new WeaponBreed();
            var wbr = new WeaponBreedRecord(store, wb);
            return wbr;
        }

        public override void DestroyAll(Predicate<WeaponBreed> condition)
        {
            throw new NotImplementedException();
        }

        public override WeaponBreedRecord Find(Predicate<WeaponBreed> condition)
        {
            return store.GetAll(ItemType.Weapon)
                .Select(ib => ib as WeaponBreed)
               .Where(wb => condition(wb))
               .Select(wbr => new WeaponBreedRecord(store, wbr))
               .First();
        }


        public override IList<WeaponBreedRecord> Order(IComparer<WeaponBreed> comparer)
        {
            throw new NotImplementedException();
        }

        public override ICollection<WeaponBreedRecord> Where(Predicate<WeaponBreed> condition)
        {
            throw new NotImplementedException();
        }
    }
}
