




using System;

namespace QS.Domain.Item
{
    /// <summary>
    /// The FlyWight of Item. 
    /// and can be called as type object pattern.
    /// </summary>
    public class ItemBreed : IItem
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
        public string UUID { get => throw new InvalidOperationException(); set => throw new InvalidOperationException(); }

        public string Img { get; set; }

        public string Prefab { get; set; }

        public string Description { get; set; }

        public IItem Clone()
        {
            throw new InvalidOperationException();
        }
    }
}
