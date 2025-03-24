

using QS.Api.WorldItem.Domain;
using QS.WorldItem;

namespace QS.Player.Inventory
{
    /// <summary>
    /// ECS 模式的就叫System，只是作为接口的就叫服务
    /// </summary>
    public interface IInventoryService
    {
        /// <summary>
        /// 添加新物品，如果是服务端，就会验证UUID。
        /// 本地的话不防挂就直接放进去
        /// </summary>
        /// <param name="item"></param>
        void AddItem(IItem item);
        /// <summary>
        /// 最近获取的物品
        /// </summary>
        /// <returns></returns>
        IItem[] GetItemsByTimeDesc();
        /// <summary>
        /// 仓库中的所有物品
        /// </summary>
        /// <returns></returns>
        IWeapon[] GetWeapons();

        /// <summary>
        /// 仓库中的所有道具
        /// </summary>
        /// <returns></returns>
        IProp[] GetProps();
    }
}