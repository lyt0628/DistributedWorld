


using GameLib.DI;
using GameLib.Pattern;
using QS.API.Data;
using QS.API.Data.Model;

namespace QS.API.DataGateway
{
    public class InventoryItemActiveRecord : AbstractActiveRecord, IItem
    {

        [Injected]
        readonly IInventoryData inventory;

        public IItem model;

        public override bool Persisted { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }
        public string UUID { get =>model.UUID; set => model.UUID=value; }
        public string Name { get => model.Name; set => model.Name=value; }
        public ItemType Type { get => model.Type; set => model.Type = value; }

        public InventoryItemActiveRecord(IItem item)
        {
            model = item;
        }

        protected override bool DoDestroy()
        {
            inventory.Remove(model);
            return true;
        }

        protected override bool DoSave()
        {
            inventory.Add(model);
            return true;
        }

        protected override void DoUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}