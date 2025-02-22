

using GameLib.DI;
using UnityEngine;
using UnityEngine.UIElements;

namespace QS.UI
{
    public class DialoguePannel : BaseDocument
    {
        Label contentLabel;
        Label speakerLabel;
        string _content = "Welcome To My Game";
        string _speaker = "lyt0628";
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                contentLabel.text = _content;
            }
        }

        public string Speaker
        {
            get { return _speaker; }
            set
            {
                _speaker = value;
                speakerLabel.text = _speaker;
            }
        }

        protected override string Address => "PUI_DialoguePannel";
        protected override bool DoPreload() => true;
        public override bool IsResident => true;
        public override bool BlockPlayerControl => true;
        protected override void OnDocumentLoaded(UIDocument document)
        {
            Hide();

            document.name = "DialoguePannel";
            contentLabel = document.rootVisualElement.Q<Label>("content");
            speakerLabel = document.rootVisualElement.Q<Label>("speaker");
            
        }

    }
}