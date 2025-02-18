using QS.Api.Inventory.Service;

namespace QS.Inventory.Service
{
    /// <summary>
    /// 背包到底要做什麼，不做什麼的話，就把它刪了
    /// 儲存物品，調用 WorldItem 提供的 API 裝成一個一個
    /// Item服務， 精煉武器，之類的東西，
    /// 儲存起來， 
    /// 添加物體，是從WorldItem 裏面查找，拿到原型，獲取它的引用
    /// 這邊是可以類型安全的，因爲物品的類型是固定的，沒必要用泛型
    /// 
    /// 背包只是用於儲存東西的地方。
    /// 但用戶在背包中幹的活可不止這些
    /// 一是 領域活動，像是，升級武器，使用物品
    /// 跟物品相關的主要是兩種
    /// 1. 查看物品的基本信息，做一些基本操作，基本上只有使用 這一個選項
    /// 2. 複雜的物品業務。
    /// 顯示角色戰鬥相關的，比如 Weapon，Seal，角色升級等東西
    /// 基本上都要單獨一個頁面來展示
    /// 遊戲不做角色的升級，因爲我討厭升級才能過劇情的設計
    /// 
    /// 序列化用NewtonJson 還是 Tomlet????
    /// 
    /// Iventory 沒有實例，自己是不知道怎麼實例化的，看一下 Json
    /// 能不能註冊mapper 了
    /// 
    /// 這些東西，限制性很多，不能直接把領域對象暴露給UI
    /// 總之，這些領域服務也定義在這一層，WorldItem專注定義物品本身的
    /// 功能，希望UI儘可能簡單，Item有 Name，根據這個來暴露接口就
    /// 可以了
    /// 按照主題來，Item主要是 UI 的定義，
    /// 大部分物品都不能自容，像是升級材料，定義了自己有多少經驗值
    /// 但是
    /// 
    /// 這些應該定義在WorldItem吧！
    /// 拿Inventory 到底要做什麼？？？？？
    /// </summary>
    class DefaultInventory : IInventory
    {
        public void AddItem(string itemName) { }
        public void RemoveItem(string itemUUID) { }

    }
}