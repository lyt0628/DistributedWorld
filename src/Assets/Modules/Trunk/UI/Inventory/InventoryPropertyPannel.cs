using QS.GameLib.View;
using QS.UI;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace QS.Trunk.UI
{
    class InventoryPropertyPannel : BaseDocument<InventoryPropertyPannel.Props, object>
    {
        public InventoryPropertyPannel(IViewNode parent, VisualElement container) : base(container, parent)
        {
            DefaultProps = new Props(string.Empty, null, string.Empty);
        }

        public record Props
        {
            public string name;

            public Props(string name, Sprite image, string desc)
            {
                this.name = name;
                this.image = image;
                this.desc = desc;
            }

            public Sprite image;
            public string desc;
        }
        const string elImageName = "itemImage";
        const string elDescName = "itemDesc";
        VisualElement elImage;
        Label elDesc;

        public override Props DefaultProps { get; }

        protected override Task OnDocumentLoaded()
        {
            elImage = Container.Q(elImageName);
            elDesc = Container.Q<Label>(elDescName);
            return Task.CompletedTask;
        }

        public override void Render()
        {
            //Debug.Log("Update InfoPannel");
            elImage.style.backgroundImage = new StyleBackground(base.props.image);
            elDesc.text = base.props.desc;
        }
    }
}