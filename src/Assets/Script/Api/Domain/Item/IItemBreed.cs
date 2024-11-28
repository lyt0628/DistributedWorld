




namespace QS.Domain.Item
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
