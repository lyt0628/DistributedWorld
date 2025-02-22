using QS.Api.WorldItem.Domain;
using QS.WorldItem;

namespace QS.Api.WorldItem.Service
{
    /// <summary>
    /// 這應當替換爲一個工廠，WorldItem 提供的API 應當只有
    /// 世界物品的定義和相關信息查詢，
    /// 以及與物品相關的服務應當在這裏實現
    /// </summary>
    public interface IItemFactory
    {
        IWeapon CreateWeapon(string name); 
        IProp CreateProp(string name);
    }
}