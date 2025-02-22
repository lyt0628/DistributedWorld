



using Cysharp.Threading.Tasks;
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
using System.Collections.Generic;
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
            // 這個語句同時聲明了依賴關係
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

        public override async void Initialize()
        {
            var h1 = Addressables.LoadAssetsAsync<TextAsset>("Conf_Weapon", null);
            h1.Completed += LoadWeapons;
            var h2 = Addressables.LoadAssetsAsync<TextAsset>("Conf_Prop", null);
            h2.Completed += LoadProps;
            

            var tasks = new List<UniTask>
            {
                h1.ToUniTask(),
                h2.ToUniTask()
            };
            // 用UniTask 可以优雅地保持时序
            await UniTask.WhenAll(tasks);
            base.Initialize();
        }
        void LoadWeapons(AsyncOperationHandle<IList<TextAsset>> handle)
        {
            foreach (var conf in handle.Result)
            {
                ParseWeapon(conf);
            }
        }

        private void ParseWeapon(TextAsset asset)
        {
            var itemDoc = tomlParser.Parse(asset.text);
            var breeds = itemDoc.GetArray("weapons");

            foreach (var breed in breeds)
            {
                breedRepo.AddWeaponBreed(TomletMain.To<DefaultWeaponBreed>(breed));
            }
        }

        void LoadProps(AsyncOperationHandle<IList<TextAsset>> handle)
        {
            foreach (var conf in handle.Result)
            {
                ParseProp(conf);
            }
        }
        private void ParseProp(TextAsset asset)
        {
            var itemDoc = tomlParser.Parse(asset.text);
            var breeds = itemDoc.GetArray("Props");

            foreach (var breed in breeds)
            {
                breedRepo.AddPropBreed(TomletMain.To<DefaultPropBreed>(breed));
            }
        }
    }
}