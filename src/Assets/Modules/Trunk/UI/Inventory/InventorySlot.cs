using QS.GameLib.View;
using QS.UI;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace QS.Trunk.UI
{
    /// <summary>
    /// new ������ UI û��Ԥ���صı�Ҫ��������ʽ��ʼ��
    /// UI ���ִ������������ڴ�й©����˲�Ҫֱ�����ö���
    /// ֻ���� UI Ԫ�ؾͺú���С�޶ȵ�ҵ�����ݾͺ�
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