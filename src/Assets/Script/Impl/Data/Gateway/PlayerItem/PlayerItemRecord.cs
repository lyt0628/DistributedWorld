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
            set => target.UUID = value;
        }
        public ItemBreed Breed
        {
            get => target.Breed;
            set => target.Breed = value;
        }

    }
}
