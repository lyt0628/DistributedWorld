using QS.Api.Common;
using QS.Api.WorldItem.Domain;
using QS.Common;
using UnityEngine.Assertions;

namespace QS.WorldItem
{
    class WorldItems : IWorldItems
    {

        readonly ItemFlyweightRepo flyweightRepo;

        public WorldItems()
        {
            flyweightRepo = WorldItemGlobal.Instance.GetInstance<ItemFlyweightRepo>();
        }

        public IAsyncOpHandle<IWeapon> CreateWeapon(string name)
        {
            WorldItemGlobal.Instance.Inject(this);

            var op = new WeaponLoadOp(this, name);
            op.Invoke();
            return op.Handle;
        }
        public IAsyncOpHandle<IProp> CreateProp(string name)
        {
            var op = new PropLoadOp(this, name);
            op.Invoke();
            return op.Handle;
        }

        public IAsyncOpHandle<IWeapon> LoadWeaponFlyweight(string name)
        {

            IWeapon flyweight = (IWeapon)flyweightRepo.Get(name);
            Assert.IsTrue(flyweight is WeaponFlyweight);


            AsyncOpBase<IWeapon> op;

            if (flyweight.image != null)
            {
                op = new UnitAsyncOp<IWeapon>(flyweight);
            }
            else
            {
                op = new WeaponFlyweightLoadOp((WeaponFlyweight)flyweight);
            }
            //op = new UnitAsyncOp<IWeapon>(flyweight);
            op.Invoke();
            return op.Handle;
        }

        public IAsyncOpHandle<IProp> LoadPropFlyweight(string name)
        {
            IProp flyweight = (IProp)flyweightRepo.Get(name);
            Assert.IsTrue(flyweight is PropFlyWeight);

            AsyncOpBase<IProp> op;

            if (flyweight.image != null)
            {
                op = new UnitAsyncOp<IProp>(flyweight);
            }
            else
            {

                op = new PropFlyweightLoadOp((PropFlyWeight)flyweight);
            }
            //op = new UnitAsyncOp<IProp>(flyweight);

            op.Invoke();
            return op.Handle;
        }

        public IAsyncOpHandle<INote> CreateNote(string name)
        {
            INote note = (INote)flyweightRepo.Get(name);
            AsyncOpBase<INote> op;

            if (note.image != null)
            {
                op = new UnitAsyncOp<INote>(note);
            }
            else
            {
                op = new NoteLoadOp((DefaultNote)note);
            }

            op.Invoke();
            return op.Handle;
        }

        public IAsyncOpHandle<IMaterial> CreateMaterial(string name)
        {
            IMaterial note = (IMaterial)flyweightRepo.Get(name);
            AsyncOpBase<IMaterial> op;

            if (note.image != null)
            {
                op = new UnitAsyncOp<IMaterial>(note);
            }
            else
            {
                op = new MaterialLoadOp((DefaultMaterial)note);
            }

            op.Invoke();
            return op.Handle;
        }
    }
}