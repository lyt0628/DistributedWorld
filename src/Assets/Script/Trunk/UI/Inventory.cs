

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
    /// ����������������ã�������е����������ݣ���Ҳ����޷�ʵʱ����
    /// ��˲�Ӧ�ó������ã���Ӧ�ó�������
    /// 
    /// �������ݵı仯��������ʾ��ĺ���?,������ܱ�֤ͬһʱ����ֻ�����Լ���������
    /// �����ǵ�Ȼû���⣬�������˶���ʵʱ��ȡ����Ȼ�����µ����ݣ������ء�
    /// ����ˣ����־ֲ����б���Ҳ�����ʵ��
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
                        .GroupBy(w => w.Name) // �� Name ����
                        .Select(g => new { Item = g.First(), Count = g.Count() }) // ͳ��ÿ�����������
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