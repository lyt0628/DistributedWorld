using QS.GameLib.Pattern;
using QS.WorldItem.Domain;

namespace QS.Api.WorldItem.Domain
{
    /// <summary>
    /// IItem is a thing that can be stored in Inventory.
    /// </summary>
    public interface IItem : IItemBreed
    {
        string UUID { get; }
    }
}
