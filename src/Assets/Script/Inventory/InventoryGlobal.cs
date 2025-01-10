


using GameLib.DI;
using QS.Api.Common;
using QS.Api.Inventory.Service;

using QS.Inventory.Service;

namespace QS.Inventory
{
    public class InventoryGlobal : ModuleGlobal<InventoryGlobal>
    {
        internal IDIContext DI { get; } = IDIContext.New();

        protected override IDIContext DIContext => DI;

        public InventoryGlobal() 
        {
            DI
                .Bind<DefaultInventory>();
        }
        public override void ProvideBinding(IDIContext context)
        {
            context.BindExternalInstance(DI.GetInstance<IInventory>());
        }
    }
}