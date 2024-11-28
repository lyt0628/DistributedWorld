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
        // ��С������ע�������
        // ֻ�ṩһ��ע���
        [Injected]
        protected readonly ItemBreedSotre store;

    }
}