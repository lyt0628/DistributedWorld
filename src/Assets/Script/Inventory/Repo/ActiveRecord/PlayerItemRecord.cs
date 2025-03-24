using QS.Api.WorldItem.Domain;
using QS.GameLib.Pattern;
using QS.GameLib.Pattern.Message;
using QS.Impl.Data.Store;
using QS.WorldItem.Domain;

namespace QS.Inventory.Repo.ActiveRecord
{
    /// <summary>
    /// ������������ԭ��(Find)��ʱ��, �õ��������������һ��Clone,
    /// ����,��Ȼ����һ����UUID,Unwrap()�õ�Domain, 
    /// Ȼ���װΪIventoryItemRecord,Save() // ��������һ�������嵽�ֿ�
    /// ����Repo��InventoryItemRecord��ʱ��, �õ�����ʵ��һ��InventoryItem
    /// Clone, ���Ը��²���ֱ�Ӹ��µ�ʵ�ʶ�����ȥ, 
    /// ����DoUpdate()ʱ, ��ֱ��ȡ������ԭ�����������,Ϊ��ǰ��Clone��һ���µ�UUID
    /// (Ϊ�˷�ֹ��ԭ�������UUIDײ��,�����һ��ԭ�͵Ļ�)
    /// 
    /// ��ߵ�����Clone����Ϊ�˷�ֹ���
    /// </summary>
    class PlayerItemRecord
         : AbstractActiveRecord<Item>, IItemAttribute
    {

        readonly InventoryStore inventory;
        readonly PlayerItemRepo repo;

        internal PlayerItemRecord(
            PlayerItemRepo repo,
            InventoryStore inventory,
            Item target)
            : base(target)
        {
            this.repo = repo;
            this.inventory = inventory;
        }
        internal PlayerItemRecord(PlayerItemRepo repo, InventoryStore inventory)
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
        /// ��������ø��·�������������,
        /// �󲿷ּ���������ʹ�ñȽϵ���ϵ���Ϣ,
        /// ���Ҳ������
        /// </summary>
        protected override void AfterUpdate()
        {
            repo.Messager.Boardcast(PlayerItemMsgs.ITEM_UPDATED,
                                    new Msg1<IItem>(target));
        }

        public string UUID
        {
            get => target.UUID;
        }

    }
}
