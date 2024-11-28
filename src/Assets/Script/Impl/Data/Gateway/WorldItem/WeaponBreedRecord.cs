using QS.Domain.Combat;
using QS.Domain.Item;
using QS.Impl.Data.Store;
using QS.Impl.Domain.Item;

namespace QS.Impl.Data.Gateway.WorldItem
{
    public class WeaponBreedRecord
        : ItemBreedRecord<WeaponBreed>, IWeaponBreed, IWeaponBreed_tag
    {
        public WeaponBreedRecord(ItemBreedSotre store, WeaponBreed breed)
            : base(store, breed)
        {
        }


        #region [[ Accessor Delegate ]]
        public IBuff MainBuff
        {
            get => target.MainBuff;
            set => target.MainBuff = value;
        }
        #endregion
    }
}