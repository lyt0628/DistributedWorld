using QS.GameLib.View;
using QS.UI;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace QS.Trunk.UI
{
    /// <summary>
    /// new 出来的 UI 没有预加载的必要，必须显式初始化
    /// UI 部分代码恨容易造成内存泄漏，因此不要直接引用对象，
    /// 只持有 UI 元素就好和最小限度的业务数据就好
    /// </summary>
    class InventorySlot : BaseDocument<InventorySlot.Props, object>
    {
        public record Props
        {
            public string uuid;

            public Props(string uuid, int amount, Sprite image)
            {
                this.uuid = uuid;
                this.amount = amount;
                this.image = image;
            }

            public int amount;
            public Sprite image;
        }

        const string slotImageName = "image";
        const string slotLabelName = "amount";
        Label lb_Amount;
        VisualElement el_Image;


        public InventorySlot(VisualTreeAsset treeAsset, IViewNode parent) : base(treeAsset, parent)
        {
            Assert.IsNotNull(parent, nameof(InventorySlot));
            DefaultProps = new Props(null, 0, null);
        }

        public new static string Address => "doc_Slot";

        public override Props DefaultProps { get; }

        protected override Task OnDocumentLoaded()
        {
            lb_Amount = Container.Q<Label>(slotLabelName);
            el_Image = Container.Q<VisualElement>(slotImageName);

            return Task.CompletedTask;
        }

        protected override bool ShouldUpdate(Props nextProps, object nextStates)
        {
            return !nextProps.Equals(base.props);
        }

        public override void Render()
        {
            lb_Amount.text = base.props.amount.ToString();
            el_Image.style.backgroundImage = new StyleBackground(base.props.image);
        }

    }
}