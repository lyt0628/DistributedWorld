using QS.Api.WorldItem.Domain;

namespace QS.Api.WorldItem.Service
{

    public interface IWorldItemService
    {
        T GetItemProto<T>(string name) where T : class, IItem;
    }
}