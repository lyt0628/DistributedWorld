using QS.Api.WorldItem.Domain;

namespace QS.WorldItem.Domain
{
    /// <summary>
    /// ����Ǳ�횵�
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
