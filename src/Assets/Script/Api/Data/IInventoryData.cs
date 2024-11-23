


using QS.Domain.Item;

namespace QS.Api.Data
{

    /// <summary>
    /// The difinition for inventory of player.
    /// </summary>
    public interface IInventoryData
    {
        void Add(IItem item);
        void Remove(IItem item);
    }

}