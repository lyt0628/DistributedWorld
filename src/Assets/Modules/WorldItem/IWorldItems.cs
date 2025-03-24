

using QS.Api.Common;
using QS.Api.WorldItem.Domain;

namespace QS.WorldItem
{
    public interface IWorldItems
    {
        IAsyncOpHandle<INote> CreateNote(string name);
        IAsyncOpHandle<IProp> CreateProp(string name);
        IAsyncOpHandle<IWeapon> CreateWeapon(string name);
        IAsyncOpHandle<IMaterial> CreateMaterial(string name);

        IAsyncOpHandle<IProp> LoadPropFlyweight(string name);
        IAsyncOpHandle<IWeapon> LoadWeaponFlyweight(string name);
    }
}