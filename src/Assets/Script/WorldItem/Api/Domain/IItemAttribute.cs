namespace QS.Api.WorldItem.Domain
{
    /// <summary>
    /// The weapon Defferent fields with item.
    /// and used as a flyWeight definition 
    /// </summary>
    public interface IItemAttribute
    {
        string UUID { get; }
    }
}