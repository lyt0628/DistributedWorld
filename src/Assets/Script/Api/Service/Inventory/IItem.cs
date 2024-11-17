


using UnityEngine;

namespace QS.API
{
   
    public interface IItem
    {
        IItemModel Model { set; }

        string Name { get; }
        string UUID { get; }
        string Description { get; }
        ItemType Type { get; }
        string SubType { get; }

        bool Usable {  get; }
        void Use();
        bool Consumable { get; }

        Sprite SpriteImg { get; }

        GameObject Prefab { get; }

    }
}