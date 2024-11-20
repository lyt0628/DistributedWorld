


using GameLib.Pattern;

namespace QS.API.DataGateway
{
    class InventoryItemActiveRecordWrapper<T>
        : InventoryItemActiveRecord
        where T : InventoryItemActiveRecord
    {
        private readonly InventoryItemActiveRecord delegat;
        public InventoryItemActiveRecordWrapper(InventoryItemActiveRecord activeRecord) 
            : base(activeRecord) 
        { 
            delegat = activeRecord;
        }
        public override int ID => delegat.ID;

        public override bool Persisted { get => delegat.Persisted; protected set { return; } }

        protected override bool DoDestroy()
        {
            return delegat.Destroy();
        }

        protected override bool DoSave()
        {
            return delegat.Save();
        }

        protected override void DoUpdate()
        {
            delegat.Update();
        }
    }
}