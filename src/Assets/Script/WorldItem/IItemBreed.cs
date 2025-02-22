using QS.Api.Common;
using QS.Api.WorldItem.Domain;
using UnityEngine;

namespace QS.WorldItem.Domain
{
    /// <summary>
    /// ����Ǳ�횵�
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
