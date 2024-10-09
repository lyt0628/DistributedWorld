using UnityEngine;

namespace QS.API
{

    public abstract class AbstractItemModel : IItemModel
    {
        public virtual string Name { get; }
        public abstract string UUID { get; }
        public abstract ItemType Type { get; }
        public abstract string SubType { get; }
        public abstract int Rank { get; }
        public abstract string Description { get; }
        public abstract bool Usable { get; }
        public abstract bool Consumable { get; }
        public abstract Sprite SpriteImg { get; }

        public abstract void Use();
    }
}