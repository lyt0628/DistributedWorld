

using QS.Api.WorldItem.Domain;
using QS.WorldItem;

namespace QS.Player.Inventory
{
    /// <summary>
    /// ECS ģʽ�ľͽ�System��ֻ����Ϊ�ӿڵľͽз���
    /// </summary>
    public interface IInventoryService
    {
        /// <summary>
        /// �������Ʒ������Ƿ���ˣ��ͻ���֤UUID��
        /// ���صĻ������Ҿ�ֱ�ӷŽ�ȥ
        /// </summary>
        /// <param name="item"></param>
        void AddItem(IItem item);
        /// <summary>
        /// �����ȡ����Ʒ
        /// </summary>
        /// <returns></returns>
        IItem[] GetItemsByTimeDesc();
        /// <summary>
        /// �ֿ��е�������Ʒ
        /// </summary>
        /// <returns></returns>
        IWeapon[] GetWeapons();

        /// <summary>
        /// �ֿ��е����е���
        /// </summary>
        /// <returns></returns>
        IProp[] GetProps();
    }
}