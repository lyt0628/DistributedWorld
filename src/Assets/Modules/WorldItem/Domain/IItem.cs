using UnityEngine;

namespace QS.Api.WorldItem.Domain
{
    /// <summary>
    /// IItem is a thing that can be stored in Inventory.
    /// </summary>
    public interface IItem
    {
        string uuid { get; }

        #region [[Shared Resources]]
        string name { get; }
        ItemType type { get; }
        Sprite image { get; }
        GameObject prefab { get; }
        string desc { get; }

        #endregion
    }
}
