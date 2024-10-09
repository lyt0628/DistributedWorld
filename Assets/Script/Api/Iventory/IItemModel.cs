

using System;
using UnityEngine;

namespace QS.API
{

   public interface IItemModel
    {

        string Name { get; }

        string  UUID { get; }
        ItemType Type { get; }
        string SubType { get; }
        int Rank { get; }
        string Description { get; }

        bool Usable { get; }

        void Use();

        bool Consumable { get; }

        Sprite SpriteImg { get; }

    }
}