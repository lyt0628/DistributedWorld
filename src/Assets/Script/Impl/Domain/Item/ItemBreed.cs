using QS.Api.Domain.Item;
using QS.Domain.Item;

namespace QS.Impl.Domain.Item
{
    /// <summary>
    /// The FlyWight of Item. 
    /// and can be called as type object pattern.
    /// </summary>
    public abstract class ItemBreed : IItemBreed, IItemBreed_tag
    {
        public ItemBreed() { }
        public ItemBreed(IItem item)
        {
            Name = item.Name;
            Type = item.Type;
            Img = item.Img;
            Prefab = item.Prefab;
            Description = item.Description;
        }

        public string Name { get; set; }
        public ItemType Type { get; set; }

        public string Img { get; set; }

        public string Prefab { get; set; }

        public string Description { get; set; }

    }
}
