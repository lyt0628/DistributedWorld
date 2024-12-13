using QS.Api.WorldItem.Domain;
using QS.GameLib.Pattern;
using QS.Impl.Data.Store;
using QS.WorldItem.Domain;

namespace QS.WorldItem.Repo.ActiveRecord
{
    abstract class ItemBreedRecord<T>
        : AbstractActiveRecord<T>, IItemBreed, IItemBreed_tag
        where T : ItemBreed
    {
        readonly ItemBreedSotre store;

        public ItemBreedRecord(ItemBreedSotre store, T target)
            : base(target)
        {
            this.store = store;
        }

        //public override bool Persisted { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }
        protected override bool DoDestroy()
        {
            store.Remove(Name);
            return true;
        }

        protected override bool DoSave()
        {
            store.Add(target);
            return true;
        }

        protected override void DoUpdate()
        {
            throw new System.InvalidOperationException();
        }


        #region [[ Accessor Delegate ]]
        public string Name
        {
            get => ((IItemBreed)target).Name;
            set => ((IItemBreed_tag)target).Name = value;
        }
        public ItemType Type
        {
            get => ((IItemBreed)target).Type;
            set => ((IItemBreed_tag)target).Type = value;
        }
        public string Img
        {
            get => ((IItemBreed)target).Img;
            set => ((IItemBreed_tag)target).Img = value;
        }
        public string Prefab
        {
            get => ((IItemBreed)target).Prefab;
            set => ((IItemBreed_tag)target).Prefab = value;
        }
        public string Description
        {
            get => ((IItemBreed)target).Description;
            set => ((IItemBreed_tag)target).Description = value;
        }
        #endregion
    }
}
