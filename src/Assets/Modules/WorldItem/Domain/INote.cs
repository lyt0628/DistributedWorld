using QS.Api.WorldItem.Domain;

namespace QS.WorldItem
{

    public interface INote : IItem
    {
        string content { get; }
    }
}