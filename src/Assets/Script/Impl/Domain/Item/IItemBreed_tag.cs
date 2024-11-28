using QS.Domain.Item;

namespace QS.Impl.Domain.Item
{

    public interface IItemBreed_tag
    {
        string Name { set; }

        ItemType Type { set; }

        string Img { set; }
        string Prefab { set; }
        string Description { set; }

    }

}
