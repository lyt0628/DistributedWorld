using GameLib.DI;
using QS.Api.WorldItem.Domain;
using QS.Common;
using QS.GameLib.Rx.Relay;
using QS.Player;
using QS.PlayerControl;
using QS.UI;
using QS.WorldItem;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using UnityEngine;
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
    /// 
    /// 子到父的数据流动的基本上的策略是，上级视图定义信号，由子组件触发
    /// </summary>

    [Scope(Lazy = false)]
    class InventoryUI : BaseDocument
    {
        public override string Address => "PUI_InventoryUI";
        protected override bool NeedPreload => false;
        public override bool BlockPlayerControl => true;
        #region [[Constants]]
        private const string slotLabelName = "name";
        private const string elTabsName = "tabs";
        private const string elSlotGridName = "slotGrid";
        private const string elInfoPannelName = "infoPannel";
        private const string itemDetail_ItemDescName = "itemDesc";
        private const string itemDetail_ItemImageName = "itemImage";
        private const string statusMessageName = "statusMessage";

        #endregion

        [Injected]
        readonly PlayerControls playerInput;
        [Injected]
        readonly IInventory inventory;
        [Injected]
        readonly IWorldItems worldItems;
        [Injected]
        readonly ViewNoteUI uiViewNote;


        VisualElement elGrid;
        VisualElement elTabs;
        VisualElement elPropPannel;

        InventoryGrid uiGrid;
        InventoryTabs uiTabs;
        InventoryPropertyPannel uiPropPannel;

        IEmitter<ItemType> tabSelected;
        IEmitter<string> itemSelected;
        IEmitter<Unit> dataUpdated;

        IItem activeItem;
        ItemType activeTab;


        public override void OnActive()
        {
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;

            playerInput.Inventory.Enable();
            PlayerUtil.FrozeCurrentCharacter();
        }

        public override void OnDeactive()
        {
            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            playerInput.Inventory.Disable();
            playerInput.Inventory_Weapon.Disable();
            playerInput.Inventory_Prop.Disable();
            PlayerUtil.UnfrozeCurrentCharacter();
        }


        protected override async Task OnDocumentLoaded()
        {
            Document.name = "UI_InventoryUI";
            await AddFakeData();

            BindPlayerInputCB();
            FindWidgets();
            BindSignalHandler();
            await LoadChildUI();
            tabSelected.Emit(ItemType.Weapon);

        }
        void BindPlayerInputCB()
        {
            playerInput.Inventory.CloseInventory.started += (_) =>
            {
                UIStack.Pop();
            };
            playerInput.Inventory_Prop.Use.started += (_) =>
            {
                if (activeItem != null && activeItem is IProp p)
                {
                    p.Use();
                    if (p.isDepletable)
                    {
                        inventory.Remove(p);
                    }
                    dataUpdated.Emit(new Unit());
                }
            };
            playerInput.Inventory_Note.view.started += (_) =>
            {
                if (activeItem != null && activeItem is INote n)
                {
                    uiViewNote.props = new ViewNoteUI.Props(n.content);
                    uiViewNote.Show();
                }
            };
        }

        private async Task LoadChildUI()
        {
            uiTabs = new InventoryTabs(this, elTabs, tabSelected);
            uiGrid = new InventoryGrid(this, elGrid, itemSelected);
            uiPropPannel = new InventoryPropertyPannel(this, elPropPannel);
            await uiTabs.LoadAsync().Task;
            await uiGrid.LoadAsync().Task;
            await uiPropPannel.LoadAsync().Task;
        }

        private async Task AddFakeData()
        {
            await WorldItemGlobal.Instance.LoadHandle.Task;
            var loadProp = worldItems.CreateProp("MpBottle");
            await loadProp.Task;
            inventory.Add(loadProp.Result);

            var loadProp2 = worldItems.CreateProp("HpBottle");
            await loadProp2.Task;
            inventory.Add(loadProp2.Result);

            var loadSword = worldItems.CreateWeapon("RustSword");
            await loadSword.Task;
            inventory.Add(loadSword.Result);

            var loadNote = worldItems.CreateNote("ManaUsage");
            await loadNote.Task;
            inventory.Add(loadNote.Result);


            var loadMat = worldItems.CreateMaterial("Steel");
            await loadMat.Task;
            inventory.Add(loadMat.Result);
        }

        private void BindSignalHandler()
        {
            Relay<string> // 顶级的事件具体操作最好在顶级视图中实现
                .Emit(out itemSelected)
                .Map(uuid =>
                {
                    activeItem = inventory.Get(uuid);
                    return activeItem;
                })
                .Subscrib((i) =>
                {
                    uiPropPannel.props = new InventoryPropertyPannel.Props(i.uuid, i.image, i.desc);
                })
                .Subscrib((i) =>
                {
                    if (i.type == ItemType.Prop)
                    {
                        playerInput.Inventory_Prop.Enable();
                    }
                    else
                    {
                        playerInput.Inventory_Prop.Disable();
                    }

                    if (i.type == ItemType.Note)
                    {
                        playerInput.Inventory_Note.Enable();
                    }
                    else
                    {
                        playerInput.Inventory_Note.Disable();
                    }
                });


            Relay<ItemType>
                   .Emit(out tabSelected)
                   .Subscrib(
                   onNext: (t) =>
                   {
                       activeTab = t;
                       switch (t)
                       {
                           case ItemType.Weapon:
                               uiGrid.props = new InventoryGrid.Props(inventory.GetWeapons());
                               break;
                           case ItemType.Prop:
                               uiGrid.props = new InventoryGrid.Props(inventory.GetProps());
                               break;
                           case ItemType.Note:
                               uiGrid.props = new InventoryGrid.Props(inventory.GetNotes());
                               break;
                           case ItemType.Material:
                               uiGrid.props = new InventoryGrid.Props(inventory.GetMaterials());
                               break;
                       }

                       if (uiGrid.ElmentProps.items.Any())
                       {
                           uiGrid.SelectAt(0);
                       }
                   },
                   onError: (e) =>
                   {
                       throw e;
                   });
            Relay<Unit> // 顶级的事件具体操作最好在顶级视图中实现
                   .Emit(out dataUpdated)
                   .Subscrib((_) =>
                   { // 之所以不把信号传递给Grid是为了保持单向数据流
                       tabSelected.Emit(activeTab);
                       if (uiGrid.SelectedSlotIndex < uiGrid.ElmentProps.items.Count)
                       {
                           uiGrid.SelectAt(uiGrid.SelectedSlotIndex);
                       }
                       else if (uiGrid.ElmentProps.items.Count > 0)
                       {
                           uiGrid.SelectAt(uiGrid.SelectedSlotIndex);
                       }
                   });
        }

        private void FindWidgets()
        {
            elTabs = Container.Q(elTabsName);
            elGrid = Container.Q(elSlotGridName);
            elPropPannel = Container.Q(elInfoPannelName);

        }

    }
}