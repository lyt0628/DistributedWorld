using QS.Api.WorldItem.Domain;

namespace QS.WorldItem.Domain
{

    interface IItemBreed_tag
    {
        string Name { set; }

        ItemType Type { set; }

        string Img { set; }
        string Prefab { set; }
        string Description { set; }

    }

}
