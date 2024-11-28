using GameLib.DI;
using QS.Domain.Item;
using QS.GameLib.Pattern;
using QS.Impl.Data.Store;

namespace QS.Impl.Data.Gateway
{
    public abstract class WorldItemRecord<T>
        : AbstractActiveRecord<T>, IItemAttribute, IItemAttribute_tag
        where T : Item
    {
        protected readonly ItemSotre store;

        protected WorldItemRecord(ItemSotre store, T target) : base(target)
        {
            this.store = store;
        }

        protected override bool DoDestroy()
        {
            store.Remove(UUID);
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


        public string UUID
        {
            get => target.UUID;
            set => target.UUID = value;
        }
        public ItemBreed Breed
        {
            get => target.Breed;
            set => target.Breed = value;
        }
    }

}