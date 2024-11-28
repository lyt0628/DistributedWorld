using GameLib.DI;
using QS.GameLib.Pattern;
using QS.Impl.Data.Store;
using QS.Impl.Domain.Item;

namespace QS.Impl.Data.Gateway.WorldItem
{
    public abstract class WorldItemRepo<T, R>
        : AbstractActiveRepo<T, R>
        where T : Item
        where R : WorldItemRecord<T>
    {
        [Injected]
        protected readonly ItemSotre store;

    }
}