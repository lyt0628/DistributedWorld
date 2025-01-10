namespace QS.Api.Inventory.Service
{

    /// <summary>
    /// 總之，重構這個inventory模塊。
    /// worlditem提供的功能有 現在只有 創建物體這麼一個功能，
    /// 所有的業務操作 都有 物體自己作爲領域對象來實現。
    /// 
    /// inventory 只需要考慮， 序列化，反序列化，用戶操作等功能，
    /// 無疑，這就是 一個repository
    /// 
    /// 因爲使用物體本身作爲領域對象來實現，所以
    /// 前面設想的， 使用無副作用的方式來操作是不可能的，
    /// 純函數和低耦合只能選擇一個
    /// 
    ///
    /// </summary>
    public interface IInventory
    {
        public void AddItem(string itemName);
        public void RemoveItem(string itemUUID);

    }
}