
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
            .BindInstance(LifecycleProvider.Instance) // �������о��ܳ���,�������ŷŵ�GameLib
                                                      // Gloal Setting
            .Bind<GlobalPhysicSetting>()
            .Bind<PlayerInstructionSetting>()
            // Data Layer
            .Bind<PlayerLocationData>()
            .Bind<PlayerInputData>()
            .Bind<PlayerCharacterData>()
            .Bind<WorldItemDataLoader>()
            .Bind<ItemBreedSotre>() // ��Щ�಻����Ҫ���⹫����
            .Bind<ItemSotre>() // �����ֳ������ĺ�,��ЩҪλ���ڲ�
            .Bind<Inventory>() // Ȼ��ʹ�� SPI, ע��ΪManager����GameManager����
             // Gata Gateway Layer
            .Bind<WeaponBreedRepo>()
            .Bind<WorldWeaponRepo>()
            .Bind<PlayerItemRepo>()
            .Bind<WorldItemRepoFacade>() // ���
            .Bind<PlayerItemRepofacade>()
            // Service Layer
            .Bind<PlayerControllService>()
            .Bind<PlayerInstructionData>()
            .Bind<CharacterTranslationDTO>(ScopeFlag.Prototype);

            GlobalDIContext.GetInstance<WorldItemDataLoader>();
        }
    }
}