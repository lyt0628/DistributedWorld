using GameLib;

namespace QS.Domain.Item
{
    /// <summary>
    /// IItem is a thing that can be stored in Inventory.
    /// </summary>
    public interface IItem : IClonable<IItem>
    {
        /// <summary>
        /// 物体的名字, 是这类物体的唯一ID
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 物体的uuid, 每个物体实例都有自己的uuid, 
        /// 唯一标识自己这个物体.
        /// 特例, 原型物体的uuid都是一样的, 只有当这个物体不在
        /// 是原型才会在为他生成独立的uuid
        /// 
        /// 如何提供这个原型物体, 它应该是一个单例, 不应该注入,
        /// 使用 一个服务来提供这个原型
        /// 
        /// 独立物体 的数据又要怎么存储, 子类应该各有各的存储, 具体表(因为是活动记录, 具体表最好)
        /// </summary>
        string UUID { get; set; }
        ItemType Type { get; }

        string Img { get; }
        string Prefab { get; }
        string Description { get; }


    }
}
