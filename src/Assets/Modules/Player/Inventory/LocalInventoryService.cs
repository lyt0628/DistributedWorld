

using QS.Api.WorldItem.Domain;
using QS.WorldItem;
using System.Collections.Generic;

namespace QS.Player.Inventory
{
    /// <summary>
    /// �����Զ�̵Ĳֿ����Ļ�����Ҫÿ�λ�ȡ���ݶ���ȫ��
    /// ��Զ�̵����ݿ��л�ȡ
    /// </summary>
    class LocalInventoryService : InventoryService
    {
        DefaultInventory m_inventory = new();

        public override void AddItem(IItem item)
        {
            m_inventory.Add(item);
        }

        public override IItem[] GetItemsByTimeDesc()
        {
            var i1 = m_inventory.GetWeapons();
            var i2 = m_inventory.GetProps();
            var result = new List<IItem>();
            result.AddRange(i1);
            result.AddRange(i2);
            return result.ToArray();
        }

        public override IProp[] GetProps()
        {
            return m_inventory.GetProps();
        }

        public override IWeapon[] GetWeapons()
        {
            return m_inventory.GetWeapons();
        }
    }
}