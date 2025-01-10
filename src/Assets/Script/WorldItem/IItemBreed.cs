using QS.Api.WorldItem.Domain;

namespace QS.WorldItem.Domain
{
    /// <summary>
    /// 類對象是必須的
    /// </summary>
    public interface IItemBreed
    {
        string Name { get; }
        ItemType Type { get; }
        string Sprite { get; }
        string Prefab { get; }
        string Description { get; }

    }

}
