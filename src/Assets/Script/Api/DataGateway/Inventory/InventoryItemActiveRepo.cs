


using GameLib.DI;
using GameLib.Pattern;
using QS.API.Data;
using System;
using System.Collections.Generic;

namespace QS.API.DataGateway
{
    public class InventoryItemActiveRepo : AbstractActiveRepo<InventoryItemActiveRecord>
    {
        [Injected]
        readonly IInventoryData inventory;

        public override InventoryItemActiveRecord Create()
        {
            return new InventoryItemActiveRecord(new Item());
        }

        public override void DestroyAll(Predicate<InventoryItemActiveRecord> condition)
        {
            throw new NotImplementedException();
        }

        public override InventoryItemActiveRecord Find(int id)
        {
            throw new NotImplementedException();
        }

        public override IList<InventoryItemActiveRecord> Order(IComparer<InventoryItemActiveRecord> comparer)
        {
            throw new NotImplementedException();
        }

        public override ICollection<InventoryItemActiveRecord> Where(Predicate<InventoryItemActiveRecord> condition)
        {
            throw new NotImplementedException();
        }
    }
}