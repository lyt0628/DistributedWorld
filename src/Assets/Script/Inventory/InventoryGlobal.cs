


using GameLib.DI;
using QS.Api.Inventory.Service;
using QS.Common;
using QS.GameLib.Pattern;
using QS.Impl.Data.Store;
using QS.Inventory.Repo;
using QS.Inventory.Repo.ActiveRecord;
using QS.Inventory.Service;

namespace QS.Inventory
{
    public class InventoryGlobal : Sington<InventoryGlobal>, IBindingProvider
    {
        internal IDIContext DI { get; } = IDIContext.New();
        public InventoryGlobal() 
        {
            DI.Bind<InventoryStore>()
              .Bind<PlayerItemRepository>()
              .Bind<PlayerItemRepo>()
              .Bind<PlayerInventoryService>();
        }
        public void ProvideBinding(IDIContext context)
        {
            context.BindExternalInstance(DI.GetInstance<IPlayerInventoryService>());
        }
    }
}