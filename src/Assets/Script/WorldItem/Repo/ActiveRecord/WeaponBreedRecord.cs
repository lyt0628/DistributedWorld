using QS.Api.Combat.Domain;
using QS.Impl.Data.Store;
using QS.WorldItem.Domain;

namespace QS.WorldItem.Repo.ActiveRecord
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