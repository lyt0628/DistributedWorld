



using GameLib.DI;
using QS.Api.Combat.Domain;
using QS.Api.Common;
using QS.Api.WorldItem;
using QS.Api.WorldItem.Domain;
using QS.Api.WorldItem.Service;
using QS.Combat;
using QS.Common;
using QS.GameLib.Pattern;
using QS.WorldItem.Domain;
using QS.WorldItem.Service;
using Tomlet;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace QS.WorldItem
{
    public class WorldItemGlobal : ModuleGlobal<WorldItemGlobal>
    {
        internal IDIContext DI { get; } = IDIContext.New();
        protected override IDIContext DIContext => DI;

        [Injected]
        readonly TomlParser tomlParser;

       
        [Injected]
        readonly DefaultItemBreedRepo breedRepo;



        public WorldItemGlobal()
        {
            // ﬂ@ÇÄ’Zæ‰Õ¨ïr¬ï√˜¡À“¿ŸáÍPÇS
            CombatGlobal.Instance.ProvideBinding(DI);

            DI
                .BindExternalInstance(DepsGlobal.Instance.GetInstance<TomlParser>())
                .Bind<DefaultItemBreedRepo>()
                .Bind<DefaultItemFactory>();

            DI.Inject(this);

        }

        public override void ProvideBinding(IDIContext context)
        {
            context
                .BindExternalInstance(DI.GetInstance<IItemBreedRepo>())
                .BindExternalInstance(DI.GetInstance<IItemFactory>());

           
        }

        public override void Initialize()
        {
            var handle = Addressables.LoadAssetAsync<TextAsset>("Conf_Items");
            handle.Completed += LoadItems;
        }

        void LoadItems(AsyncOperationHandle<TextAsset> handle)
        {

            Assert.AreEqual(AsyncOperationStatus.Succeeded, handle.Status,
                "Failed to load items configuration!!!");

            //Debug.Log(handle.State.text);
            var itemDoc = tomlParser.Parse(handle.Result.text);
            var breeds = itemDoc.GetArray("weapons");

            foreach (var breed in breeds)
            {
                breedRepo.AddWeaponBreed(TomletMain.To<DefaultWeaponBreed>(breed));
            }


            base.Initialize();
        }

    }
}