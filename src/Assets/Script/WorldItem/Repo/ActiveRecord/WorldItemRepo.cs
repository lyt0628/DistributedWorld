using GameLib.DI;
using QS.GameLib.Pattern;
using QS.Impl.Data.Store;
using QS.WorldItem.Domain;

namespace QS.WorldItem.Repo.ActiveRecord
{
    abstract class WorldItemRepo<T, R>
        : AbstractActiveRepo<T, R>
        where T : Item
        where R : WorldItemRecord<T>
    {
        [Injected]
        protected readonly ItemSotre store;

    }
}