using QS.UI;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace QS.Trunk.UI
{
    class ViewNoteUI : BaseDocument<ViewNoteUI.Props, object>
    {
        public ViewNoteUI()
        {
            DefaultProps = new Props(string.Empty);
        }

        public record Props
        {
            public string content;

            public Props(string content)
            {
                this.content = content;
            }
        }

        public override string Address => "V_NoteView";

        public override Props DefaultProps { get; }

        Label el_Label;

        protected override Task OnDocumentLoaded()
        {
            el_Label = Container.Q<Label>("content");

            return Task.CompletedTask;
        }

        public override void Render()
        {
            base.Render();

            el_Label.text = base.props.content;
        }
    }
}