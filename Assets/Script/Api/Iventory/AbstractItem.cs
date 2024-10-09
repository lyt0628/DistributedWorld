


using UnityEngine;

namespace QS.API
{
   public abstract  class AbstractItem : IItem
    {
        public virtual IItemModel Model { protected get; set; }

        public string Name => Model.Name;

        public string UUID => Model.UUID;

        public string Description => Model.Description;

        public ItemType Type => Model.Type;

        public string SubType => Model.SubType;

        public Sprite sprite => Model.SpriteImg;

        public bool Usable => Model.Usable;
        public virtual void Use() => Model.Use();
        public bool Consumable => Model.Consumable;

    }
}