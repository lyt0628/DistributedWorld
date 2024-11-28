
using GameLib.DI;
using QS.GameLib.Pattern;
using QS.Impl.Data;
using QS.Impl.Data.Gateway.Facade;
using QS.Impl.Data.Gateway.PlayerItem;
using QS.Impl.Data.Gateway.WorldItem;
using QS.Impl.Data.Loader;
using QS.Impl.Data.Store;
using QS.Impl.Service;
using QS.Impl.Service.DTO;
using QS.Impl.Setting;

namespace QS.Impl
{
    public class ImplGlobal : Sington<ImplGlobal>
    {
        public IDIContext GlobalDIContext { get; } = IDIContext.New();

        public ImplGlobal() { 
            GlobalDIContext
            .BindInstance(LifecycleProvider.Instance) // 这个组件感觉很抽象,可以试着放到GameLib
                                                      // Gloal Setting
            .Bind<GlobalPhysicSetting>()
            .Bind<PlayerInstructionSetting>()
            // Data Layer
            .Bind<PlayerLocationData>()
            .Bind<PlayerInputData>()
            .Bind<PlayerCharacterData>()
            .Bind<WorldItemDataLoader>()
            .Bind<ItemBreedSotre>() // 这些类不是想要对外公布的
            .Bind<ItemSotre>() // 做出分成上下文后,这些要位于内部
            .Bind<Inventory>() // 然后使用 SPI, 注册为Manager来到GameManager调用
             // Gata Gateway Layer
            .Bind<WeaponBreedRepo>()
            .Bind<WorldWeaponRepo>()
            .Bind<PlayerItemRepo>()
            .Bind<WorldItemRepoFacade>() // 外观
            .Bind<PlayerItemRepofacade>()
            // Service Layer
            .Bind<PlayerControllService>()
            .Bind<PlayerInstructionData>()
            .Bind<CharacterTranslationDTO>(ScopeFlag.Prototype);

            GlobalDIContext.GetInstance<WorldItemDataLoader>();
        }
    }
}