using QS.Api.WorldItem.Domain;

namespace QS.WorldItem.Domain
{
    /// <summary>
    /// The FlyWight of BaseItem. 
    /// and can be called as type object pattern.
    /// </summary>
    public abstract class BaseItemBreed : IItemBreed
    {
        
        public string Name { get; set; }
        public ItemType Type { get; set; }

        public string Sprite { get; set; }

        public string Prefab { get; set; }

        public string Description { get; set; }

    }
}
