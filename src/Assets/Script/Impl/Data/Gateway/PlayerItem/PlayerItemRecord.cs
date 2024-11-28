using GameLib.DI;
using QS.Api.Domain.Item;
using QS.Domain.Item;
using QS.GameLib.Pattern;
using QS.GameLib.Pattern.Message;
using QS.GameLib.Util;
using QS.Impl.Data.Store;

namespace QS.Impl.Data.Gateway
{
    /// <summary>
    /// 从世界物体拿原型(Find)的时候, 拿到的是世界物体的一个Clone,
    /// 但是,仍然持有一样的UUID,Unwrap()拿到Domain, 
    /// 然后包装为IventoryItemRecord,Save() // 完成了添加一个新物体到仓库
    /// 当从Repo拿InventoryItemRecord的时候, 拿到的其实是一个InventoryItem
    /// Clone, 所以更新不会直接更新到实际对象上去, 
    /// 当我DoUpdate()时, 会直接取消掉对原本对象的引用,为当前的Clone做一个新的UUID
    /// (为了防止与原本物体的UUID撞上,如果是一个原型的话)
    /// 
    /// 这边的两个Clone都是为了防止脏读
    /// </summary>
    public class PlayerItemRecord
         : AbstractActiveRecord<Item>, IItemAttribute, IItemAttribute_tag
    {

        readonly Inventory inventory;
        readonly PlayerItemRepo repo;

        internal PlayerItemRecord(
            PlayerItemRepo repo, 
            Inventory inventory,
            Item target) 
            : base(target)
        {
            this.repo = repo;
            this.inventory = inventory;
        }
        internal PlayerItemRecord(PlayerItemRepo repo, Inventory inventory) 
        {
            this.repo = repo;
            this.inventory = inventory;
        }

        protected override bool DoDestroy()
        {
            inventory.Remove(UUID);
            return true;
        }

        protected override bool DoSave()
        {
            inventory.Add(target);
            return true;
        }

        protected override void DoUpdate()
        {
            inventory.Remove(UUID);
            inventory.Add(target);
        }

        /// <summary>
        /// 用这个后置更新方法来做监听器,
        /// 大部分监听器都是使用比较低耦合的消息,
        /// 这边也不例外
        /// </summary>
        protected override void AfterUpdate()
        {
            repo.Messager.Boardcast(PlayerItemMsgs.ITEM_UPDATED, 
                                    new Msg1<IItem>(target));
        }

        public string UUID
        {
            get => target.UUID;
            set => target.UUID = value;
        }
        public ItemBreed Breed
        {
            get => target.Breed;
            set => target.Breed = value;
        }

    }
}
