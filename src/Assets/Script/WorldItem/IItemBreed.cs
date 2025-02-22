using QS.Api.Common;
using QS.Api.WorldItem.Domain;
using UnityEngine;

namespace QS.WorldItem.Domain
{
    /// <summary>
    /// 類對象是必須的
    /// </summary>
    public interface IItemBreed : IResourceInitializer
    {
        string Name { get; }
        ItemType Type { get; }
        Sprite Image { get; }
        string Prefab { get; }
        string Description { get; }

    }

}
