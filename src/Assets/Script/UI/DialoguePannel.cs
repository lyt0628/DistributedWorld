

using GameLib.DI;
using UnityEngine;
using UnityEngine.UIElements;

namespace QS.UI
{
    [Scope(Value = ScopeFlag.Sington, Lazy = false)]
    public class DialoguePannel : BaseDocument
    {
        Label contentLabel;
        string _content = "Welcome To My Game";
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                contentLabel.text = _content;
            }
        }

        protected override string Address => "PUI_DialoguePannel";
        protected override bool DoPreload() => true;
        public override bool IsResident => true;
        public override bool BlockPlayerControl => true;
        protected override void OnDocumentLoaded(UIDocument document)
        {
            document.name = "DialoguePannel";
            contentLabel = document.rootVisualElement.Q<Label>("Content");

            document.rootVisualElement.RegisterCallback<ClickEvent>(_ => Hide());

            Hide();
        }

    }
}