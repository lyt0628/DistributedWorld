

using Cysharp.Threading.Tasks;
using GameLib.DI;
using QS.Api.WorldItem;
using QS.Api.WorldItem.Domain;
using QS.Api.WorldItem.Service;
using QS.Common;
using QS.Player;
using QS.PlayerControl;
using QS.UI;
using QS.WorldItem;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace QS.Trunk.UI
{
    /// <summary>
    /// 背包持有物体的引用，因而持有的是最终数据，但也因此无法实时更新
    /// 因此不应该持有引用，而应该持有数据
    /// 
    /// 已有数据的变化用流来表示真的好吗?,如果我能保证同一时间内只有我自己会持有这个
    /// 数据那当然没问题，其他的人都是实时获取，自然是最新的数据，嗯嗯呢。
    /// 先如此，保持局部持有本身也是最佳实践
    /// </summary>

    [Scope(Lazy = false)]
    class InventoryUI : BaseDocument
    {
        protected override string Address => "PUI_Inventory";
        protected override bool DoPreload() => true;
        public override bool BlockPlayerControl => true;

        [Injected]
        readonly PlayerControls playerControls;
        [Injected]
        readonly IInventory inventory;
        [Injected]
        readonly IItemFactory itemFactory;

        protected override void DoShow()
        {
            base.DoShow();
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        }

        protected override void DoHide()
        {
            base.DoHide();
                        UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }


        protected override void OnDocumentLoaded(UIDocument document)
        {
            Hide();
            MiscUtil.CallAfterResourceStarted(WorldItemGlobal.Instance, Init);

            playerControls.MainUI.OpenInventory.started += (_) =>
            {
                if (!IsVisible)
                {
                    Show();
                }
                else
                {
                    Hide();
                }
            };
        }

        async void Init()
        {
            //var sword = itemFactory.CreateProp("HpBottle");
            //inventory.Add(sword);
            //sword = itemFactory.CreateProp("HpBottle");
            //inventory.Add(sword);

            var root = Document.rootVisualElement;

            VisualElement itemGrid = root.Query<VisualElement>("itemGrid");
            Label itemDesc = root.Q<Label>("itemDesc");
            VisualElement itemImage = root.Q("itemImage");
            var h = Addressables.LoadAssetAsync<VisualTreeAsset>("Slot");
            await h.ToUniTask();

            var weaponCounts = inventory.GetProps()
                        .GroupBy(w => w.Name) // 按 Name 分组
                        .Select(g => new { Item = g.First(), Count = g.Count() }) // 统计每个分组的数量
                        .ToList();


            foreach (var pair in weaponCounts)
            {
                var item = pair.Item;
                var el = h.Result.Instantiate();
                var label = el.Q<Label>("name");
                var image = el.Q("image");
                MiscUtil.CallAfterResourceStarted(item, () =>
                {
                    image.style.backgroundImage = new StyleBackground(item.Image);
                    label.text = pair.Count.ToString();
                });

                el.RegisterCallback<ClickEvent>((e) =>
                {
                    itemDesc.text = item.Description;
                    itemImage.style.backgroundImage = new StyleBackground(item.Image);
                });

                itemGrid.Add(el);
            }


        }
    }
}