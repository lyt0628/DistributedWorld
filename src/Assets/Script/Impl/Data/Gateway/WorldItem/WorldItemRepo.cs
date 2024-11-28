using GameLib.DI;
using QS.Domain.Item;
using QS.GameLib.Pattern;
using QS.Impl.Data.Store;

namespace QS.Impl.Data.Gateway
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