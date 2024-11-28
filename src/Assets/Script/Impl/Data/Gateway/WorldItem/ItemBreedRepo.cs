using GameLib.DI;
using QS.Domain.Item;
using QS.GameLib.Pattern;
using QS.Impl.Data.Store;

namespace QS.Impl.Data.Gateway
{
    public abstract class ItemBreedRepo<T, R>
        : AbstractActiveRepo<T, R>
        where T : ItemBreed
        where R : ItemBreedRecord<T>
    {
        // 减小对依赖注入的依赖
        // 只提供一个注入点
        [Injected]
        protected readonly ItemBreedSotre store;

    }
}