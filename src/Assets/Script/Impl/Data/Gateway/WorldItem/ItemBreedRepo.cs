using GameLib.DI;
using QS.GameLib.Pattern;
using QS.Impl.Data.Store;
using QS.Impl.Domain.Item;

namespace QS.Impl.Data.Gateway.WorldItem
{
    public abstract class ItemBreedRepo<T, R>
        : AbstractActiveRepo<T, R>
        where T : ItemBreed
        where R : ItemBreedRecord<T>
    {
        // ��С������ע�������
        // ֻ�ṩһ��ע���
        [Injected]
        protected readonly ItemBreedSotre store;

    }
}