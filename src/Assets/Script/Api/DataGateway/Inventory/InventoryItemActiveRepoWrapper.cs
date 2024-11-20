


using GameLib.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QS.API.DataGateway
{
    class InventoryItemActiveRepoWrapper<T>
        : InventoryItemActiveRepo
        where T : InventoryItemActiveRecord
    {
        private readonly InventoryItemActiveRepo delegat;
        public InventoryItemActiveRepoWrapper(InventoryItemActiveRepo activeRepo) {

            delegat = activeRepo;
            var ctx = GameManager.Instance.GlobalDIContext;
            ctx.Inject(delegat);
        }

        public override InventoryItemActiveRecord Create()
        {
            var ctx = GameManager.Instance.GlobalDIContext;
            var r = delegat.Create();
            ctx.Inject(r);
            
           return new InventoryItemActiveRecordWrapper<T>(r);
        }

        public override void DestroyAll(Predicate<InventoryItemActiveRecord> condition)
        {
            delegat.DestroyAll(condition);
        }

        public override InventoryItemActiveRecord Find(int id)
        {
            return new InventoryItemActiveRecordWrapper<T>(delegat.Find(id));
        }

        public override IList<InventoryItemActiveRecord> Order(IComparer<InventoryItemActiveRecord> comparer)
        {
            return delegat.Order(comparer)
                .Select(r => new InventoryItemActiveRecordWrapper<T>(r))
                .ToList<InventoryItemActiveRecord>();
        }

        public override ICollection<InventoryItemActiveRecord> Where(Predicate<InventoryItemActiveRecord> condition)
        {
            return delegat.Where(condition)
                    .Select(r=>new InventoryItemActiveRecordWrapper<T>(r))
                    .ToList<InventoryItemActiveRecord>(); 
        }

        public T CreateT()
        {
            return (T)Create();
        }

    }
}