


using Cysharp.Threading.Tasks;
using QS.Api.WorldItem.Domain;
using QS.GameLib.Rx.Relay;
using QS.GameLib.View;
using QS.UI;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace QS.Trunk.UI
{
    class InventoryTabs : BaseDocument
    {
        const string weaponTabName = "weaponTab";
        const string propTabName = "propTab";
        const string materialTabName = "materialTab";
        const string sealTabName = "sealTab";
        const string noteTabName = "noteTab";
        private const string selectedTabBgAddress = "ui_img_inventoy_tab_selected";
        private const string unselectedTabBgAddress = "ui_img_inventoy_tab_unselected";
        VisualElement weaponTab;
        VisualElement propTab;
        VisualElement materialTab;
        VisualElement sealTab;
        VisualElement noteTab;

        readonly IEmitter<ItemType> selectedTab;

        Sprite activeTabBg;
        Sprite unactiveTabBg;


        public InventoryTabs(IViewNode parent, VisualElement container, IEmitter<ItemType> emitter) : base(container, parent)
        {
            selectedTab = emitter;
        }

        protected override async Task OnDocumentLoaded()
        {
            weaponTab = Container.Q(weaponTabName);
            propTab = Container.Q(propTabName);
            materialTab = Container.Q(materialTabName);
            sealTab = Container.Q(sealTabName);
            noteTab = Container.Q(noteTabName);


            var h_SelectedTabBg = Addressables.LoadAssetAsync<Sprite>(selectedTabBgAddress);
            var h_UnselectedTabBg = Addressables.LoadAssetAsync<Sprite>(unselectedTabBgAddress);
            await UniTask.WhenAll(
                h_SelectedTabBg.Task.AsUniTask(),
                h_UnselectedTabBg.Task.AsUniTask()
            );

            activeTabBg = h_SelectedTabBg.Result;
            unactiveTabBg = h_UnselectedTabBg.Result;

            weaponTab.RegisterCallback<ClickEvent>((_) =>
            {
                selectedTab.Emit(ItemType.Weapon);
                weaponTab.style.backgroundImage = new StyleBackground(activeTabBg);
                propTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
                materialTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
                sealTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
                noteTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
            });
            propTab.RegisterCallback<ClickEvent>((_) =>
            {
                selectedTab.Emit(ItemType.Prop);

                weaponTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
                propTab.style.backgroundImage = new StyleBackground(activeTabBg);
                materialTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
                sealTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
                noteTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
            });
            materialTab.RegisterCallback<ClickEvent>((_) =>
            {
                selectedTab.Emit(ItemType.Material);

                weaponTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
                propTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
                materialTab.style.backgroundImage = new StyleBackground(activeTabBg);
                sealTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
                noteTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
            });
            sealTab.RegisterCallback<ClickEvent>((_) =>
            {
                selectedTab.Emit(ItemType.Seal);

                weaponTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
                sealTab.style.backgroundImage = new StyleBackground(activeTabBg);
                propTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
                materialTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
                noteTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
            });
            noteTab.RegisterCallback<ClickEvent>((_) =>
            {
                selectedTab.Emit(ItemType.Note);

                weaponTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
                propTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
                materialTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
                sealTab.style.backgroundImage = new StyleBackground(unactiveTabBg);
                noteTab.style.backgroundImage = new StyleBackground(activeTabBg);
            });
        }
    }
}