


using Cysharp.Threading.Tasks;
using QS.Api.WorldItem.Domain;
using QS.Common.Util;
using QS.GameLib.Rx.Relay;
using QS.GameLib.View;
using QS.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace QS.Trunk.UI
{
    class InventoryGrid : BaseDocument<InventoryGrid.Props, object>
    {

        public record Props
        {

            public IList<IItem> items;

            public Props(IList<IItem> items)
            {
                this.items = items ?? throw new ArgumentNullException(nameof(items));
            }
        }

        ObjectPool<InventorySlot> uiSlotPool;
        readonly IEmitter<string> selectedItem;

        public int SelectedSlotIndex { get; private set; } = 0;

        public override Props DefaultProps { get; }

        public InventoryGrid(IViewNode parent, VisualElement element, IEmitter<string> selectedItem) : base(element, parent)
        {
            this.selectedItem = selectedItem;
            DefaultProps = new Props(new List<IItem>());
        }

        /// <summary>
        /// ���±�ѡ�����壬�±�� 0 ��ʼ����
        /// UI ��ʾ������һ�������Է� UI �¼������ݶ�Ҫ��props �� states ��
        /// </summary>
        /// <param amount="index"></param>
        /// <returns></returns>
        public void SelectAt(int index)
        {
            if (base.props.items.Count < index + 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            var item = base.props.items[index];

            SelectedSlotIndex = index;
            selectedItem.Emit(item.uuid);
        }

        protected override async Task OnDocumentLoaded()
        {
            var h = Addressables.LoadAssetAsync<VisualTreeAsset>("Slot");
            await h.Task;


            uiSlotPool = new ObjectPool<InventorySlot>(() =>
            {
                var slot = new InventorySlot(h.Result, this);
                slot.LoadAsync();
                return slot;
            });
        }


        protected override bool ShouldUpdate(Props nextProps, object nextStates)
        {
            if (base.props.items == null) return true;

            var newItems = nextProps.items;
            var oldItems = base.props.items;

            if (newItems.Count() != oldItems.Count()) return true;

            for (var i = 0; i < oldItems.Count(); i++)
            {
                if (oldItems[i] != newItems[i]) return true;
            }

            return false;
        }

        public override void Render()
        {
            // ����ȫ�����
            var items = base.props.items;
            PadSlots(items);
            items.Zip(Container.Children()
                        .Select(c => c.userData)
                        .Cast<InventorySlot>(),
                     (item, uiSlot) => (item, uiSlot))
                .ToList()
                .ForEach(pair =>
                {
                    pair.uiSlot.props = new InventorySlot.Props(pair.item.uuid,
                                                                          2,
                                                                          pair.item.image);
                });

            // Slot ��һֱ�仯������¼���Grid �����ͺ�
            Container.RegisterCallback<ClickEvent>((e) =>
            {
                // ��Ϊ�Ǳհ����棬������ò�Ҫ���ⲿ������������
                var uiSlot = Container.Children()
                    .Select(c => c.userData)
                    .Cast<InventorySlot>()
                    .FirstOrDefault(slot => slot.Container.Contains((VisualElement)e.target));
                if (uiSlot == default) return;

                SelectedSlotIndex = Container.IndexOf(uiSlot.Container);
                selectedItem.Emit(uiSlot.ElmentProps.uuid);
            });


            void PadSlots(IEnumerable<IItem> items)
            {

                switch (items.Count())
                {
                    case 0:
                        {
                            Container.Children()
                                .Select(c => c.userData)
                                .Cast<InventorySlot>()
                                .ToList()
                                .ForEach(o => uiSlotPool.Release(o));

                            Container.Clear();
                            break;
                        }

                    default:
                        if (items.Count() < Container.childCount)
                        {
                            Container.Children()
                                .Skip(Container.childCount - items.Count())
                                .Select(c => c.userData)
                                .Cast<InventorySlot>()
                                .ToList()
                                .ForEach(uiSlot =>
                                {
                                    uiSlotPool.Release(uiSlot);
                                    Container.Remove(uiSlot.Container);
                                });
                        }
                        else if (items.Count() > Container.childCount)
                        {
                            var padCount = items.Count() - Container.childCount;
                            for (var i = 0; i < padCount; i++)
                            {

                                var uiSlot = uiSlotPool.Get();

                                Container.Add(uiSlot.Container);
                            }
                        }

                        break;
                }
            }

        }
    }
}