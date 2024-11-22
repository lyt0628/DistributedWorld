

using QS.API.Data.Model;

namespace QS.API.Data
{
    interface IInventoryData
    {
        void Add(IItem item);
        void Remove(IItem item);
    }
}   