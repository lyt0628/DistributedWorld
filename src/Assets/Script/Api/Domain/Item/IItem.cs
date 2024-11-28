using QS.Domain.Item;
using QS.GameLib.Pattern;

namespace QS.Api.Domain.Item
{
    /// <summary>
    /// IItem is a thing that can be stored in Inventory.
    /// </summary>
    public interface IItem : IItemAttribute, IItemBreed
    {

    }
}
