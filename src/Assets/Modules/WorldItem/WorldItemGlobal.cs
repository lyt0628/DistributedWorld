



using Cysharp.Threading.Tasks;
using GameLib.DI;
using QS.Api.Common;
using QS.Common;
using System.Collections.Generic;
using Tomlet;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace QS.WorldItem
{
    public class WorldItemGlobal : ModuleGlobal<WorldItemGlobal>
    {

        class WorldItemModuleLoadOp : AsyncOpBase<IModuleGlobal>
        {
            readonly WorldItemGlobal worldItemGlobal;
            [Injected]
            readonly TomlParser tomlParser;

            [Injected]
            readonly ItemFlyweightRepo fwRepo;
            [Injected]
            readonly WorldItems worldItems;

            public WorldItemModuleLoadOp(WorldItemGlobal module)
            {
                worldItemGlobal = module;
            }

            protected override async void Execute()
            {
                Instance.Inject(this);
                await DepsGlobal.Instance.LoadHandle.Task;

                var h_Weapons = Addressables.LoadAssetsAsync<TextAsset>(ConfigConstants.WEAPON_CONFIG_TAG, null);
                h_Weapons.Completed += LoadWeapons;
                var h_Props = Addressables.LoadAssetsAsync<TextAsset>(ConfigConstants.PROP_CONFIG_TAG, null);
                h_Props.Completed += LoadProps;
                var h_Notes = Addressables.LoadAssetsAsync<TextAsset>(ConfigConstants.Note_CONFIG_TAG, null);
                h_Notes.Completed += LoadNotes;
                var h_Material = Addressables.LoadAssetsAsync<TextAsset>(ConfigConstants.MATERIAL_CONFIG_TAG, null);
                h_Material.Completed += LoadMaterials;
                await UniTask.WhenAll(
                    h_Weapons.ToUniTask(),
                    h_Props.ToUniTask(),
                    h_Notes.ToUniTask(),
                    h_Material.ToUniTask()
                );
                //BindLuaEnv();
                Complete(worldItemGlobal);
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
                var weapons = itemDoc.GetArray(ConfigConstants.WEAPON_ARRAY);

                foreach (var w in weapons)
                {
                    var wf = TomletMain.To<WeaponFlyweight>(w);
                    fwRepo.Add(wf);
                }
            }

            void LoadNotes(AsyncOperationHandle<IList<TextAsset>> handle)
            {
                foreach (var conf in handle.Result)
                {
                    ParseNote(conf);
                }
            }

            private void ParseNote(TextAsset asset)
            {
                var itemDoc = tomlParser.Parse(asset.text);
                var weapons = itemDoc.GetArray(ConfigConstants.NOTE_ARRAY);

                foreach (var w in weapons)
                {
                    var wf = TomletMain.To<DefaultNote>(w);
                    fwRepo.Add(wf);
                }
            }

            void LoadMaterials(AsyncOperationHandle<IList<TextAsset>> handle)
            {
                foreach (var conf in handle.Result)
                {
                    ParseMaterial(conf);
                }
            }

            private void ParseMaterial(TextAsset asset)
            {
                var itemDoc = tomlParser.Parse(asset.text);
                var weapons = itemDoc.GetArray(ConfigConstants.MATRIAL_ARRAY);

                foreach (var w in weapons)
                {
                    var wf = TomletMain.To<DefaultMaterial>(w);
                    fwRepo.Add(wf);
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
                var breeds = itemDoc.GetArray(ConfigConstants.PROP_ARRAY);

                foreach (var breed in breeds)
                {
                    fwRepo.Add(TomletMain.To<PropFlyWeight>(breed));
                }
            }
        }

        public WorldItemGlobal()
        {
            LoadOp = new WorldItemModuleLoadOp(this);
        }

        protected override AsyncOpBase<IModuleGlobal> LoadOp { get; }

        /// <summary>
        /// 封装依靠接口设计来实现，不能依靠依赖注入
        /// </summary>
        /// <param name="global"></param>
        protected override void DoSetupBinding(IDIContext globalContext, IDIContext moduleContext)
        {

            globalContext
                .Bind<WorldItems>();

            moduleContext
                .Bind<ItemFlyweightRepo>();
        }

    }
}