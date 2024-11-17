


using UnityEngine;

namespace QS.API
{
   public abstract  class AbstractItem : IItem
    {
        public virtual IItemModel Model { get; set; }

        public string Name => Model.Name;

        public string UUID => Model.UUID;

        public string Description => Model.Description;

        public ItemType Type => Model.Type;

        public string SubType => Model.SubType;

        public Sprite SpriteImg => Model.SpriteImg;

        public bool Usable => Model.Usable;
        public void Use() => Model.Use();
        public bool Consumable => Model.Consumable;

        public GameObject Prefab => Model.Prefab;
    }
}