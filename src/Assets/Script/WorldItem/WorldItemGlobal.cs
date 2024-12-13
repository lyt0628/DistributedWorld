



using GameLib.DI;
using QS.Api.Common;
using QS.Api.WorldItem.Service;
using QS.Common;
using QS.GameLib.Pattern;
using QS.Impl.Data.Store;
using QS.WorldItem.Repo.ActiveRecord;
using QS.WorldItem.Service;

namespace QS.WorldItem
{
    public class WorldItemGlobal : ModuleGlobal<WorldItemGlobal>
    {
        internal IDIContext DI { get; } = IDIContext.New();

        protected override IDIContext DIContext => DI;

        public WorldItemGlobal()
        {
            DI.Bind<ItemSotre>()
              .Bind<WorldWeaponRepo>()
              .Bind<WorldItemService>();
        }

        public override void ProvideBinding(IDIContext context)
        {
            context.BindExternalInstance(DI.GetInstance<IWorldItemService>());
        }

    }
}