using QS.Api.WorldItem.Domain;

namespace QS.WorldItem.Domain
{

    public interface IItemBreed
    {
        string Name { get; }

        ItemType Type { get; }

        string Img { get; }
        string Prefab { get; }
        string Description { get; }

    }

}
