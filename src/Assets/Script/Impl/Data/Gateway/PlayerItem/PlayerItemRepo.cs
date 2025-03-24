using GameLib.DI;
using QS.GameLib.Pattern;
using QS.GameLib.Pattern.Message;
using QS.Impl.Data.Store;
using QS.Impl.Domain.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QS.Impl.Data.Gateway.PlayerItem
{
    public class PlayerItemRepo
         : AbstractActiveRepo<Item, PlayerItemRecord>,
        IMessagerProvider // ��Ϣ�����Ǽ���,�Ѿ����൱����Ķ�����, ����ʵ���ϲ�������
    {

        [Injected]
        readonly Inventory inventory;

        public IMessager Messager { get; } = new Messager();

        public override PlayerItemRecord Create()
        {
            return new PlayerItemRecord(this, inventory);
        }

        public override void DestroyAll(Predicate<Item> condition)
        {
            throw new NotImplementedException();
        }

        public override PlayerItemRecord Find(Predicate<Item> condition)
        {
            var i = inventory.GetAll()
                     .Where(i => condition(i))
                     .First()
                     .Clone();


            // �����������ֹ���
            var r = Create();
            r.Wrap(i);
            return r;
        }

        public override IList<PlayerItemRecord> Order(IComparer<Item> comparer)
        {
            throw new NotImplementedException();
        }

        public override ICollection<PlayerItemRecord> Where(Predicate<Item> condition)
        {
            return inventory.GetAll()
                .Where(i => condition(i))
                .Select(i => new PlayerItemRecord(this, inventory, i))
                .ToList();
        }
    }
}