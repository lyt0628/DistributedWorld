




using QS.Domain.Item;

namespace QS.Api.Data
{
    /// <summary>
    /// IWorldItemData provides the All information about Item settings.
    /// </summary>
    public interface IWorldItemData
    {
        /// <summary>
        /// Find a World Item  By Name, that is a clone of original item
        /// </summary>
        /// <param name="name">Name of the item</param>
        /// <returns>The cloned item</returns>
        IItem Find(string name);
    }
}