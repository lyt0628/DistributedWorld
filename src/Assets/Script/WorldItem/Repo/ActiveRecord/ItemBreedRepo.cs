using GameLib.DI;
using QS.GameLib.Pattern;
using QS.Impl.Data.Store;
using QS.WorldItem.Domain;

namespace QS.WorldItem.Repo.ActiveRecord
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