using GameLib.DI;
using QS.Common;
using QS.Common.Util;
using QS.Player;
using QS.PlayerControl;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace QS.UI
{
    public class OptionSelectedEvent : UnityEvent<int> { }

    /// <summary>
    /// UI也应当由对应的模块实现，这样子才能够保证高度内聚
    /// </summary>
    //[Scope(Lazy = false)]
    [Obsolete]
    public class DialogPanel : BaseDocument<DialogPanel.Props, DialogPanel.States>
    {
        [Injected]
        readonly PlayerControls playerActions;
        [Injected]
        readonly ITimer timer;

        public record Props
        {
            public string speaker;

            public Props(string speaker, string[] content)
            {
                this.speaker = speaker;
                this.lines = content;
            }

            public string[] lines;
        }

        public record States
        {
            public string displayedText = string.Empty;
            public int currentLine = 0;
        }

        public DialogPanel()
        {
            DefaultProps = new Props(string.Empty, new string[0]);
        }

        Label contentLabel;
        Label speakerLabel;

        const float c_PrintInterval = 0.5f;

        public UnityEvent OnDisplayFullCotent = new();
        public OptionSelectedEvent OnOptionSelected = new();

        protected override Task OnDocumentLoaded()
        {
            Document.name = "UI_DialoguePannel";
            contentLabel = Container.Q<Label>("content");
            speakerLabel = Container.Q<Label>("speaker");
            playerActions.DialoguePanel.Continue.started += (ctx) =>
            {
                states.currentLine++;
                if (states.currentLine >= props.lines.Length)
                {
                    UIStack.Pop();
                }
                else
                {
                    states.displayedText = string.Empty;

                }
                states.currentLine %= props.lines.Length;
            };
            timer.OnTick.AddListener(MarkDirty);
            return Task.CompletedTask;
        }


        protected override void LoadStatesFromProps(Props props, out States states)
        {
            states = new States()
            {
                displayedText = string.Empty,
                currentLine = 0
            };
            // 已经启动，表示现在的Props 
            timer.Clear();
            timer.Set(c_PrintInterval);

        }

        public override void OnActive()
        {
            //StartPrintCounter();
            timer.Set(c_PrintInterval);
            playerActions.DialoguePanel.Continue.Enable();
            PlayerUtil.FrozeCurrentCharacter();
            MiscUtil.ShowCursor();
        }


        public override void OnDeactive()
        {
            //StopCurrentPrintCounterIfAny();
            timer.Clear();
            playerActions.DialoguePanel.Continue.Disable();
            PlayerUtil.UnfrozeCurrentCharacter();
            MiscUtil.HideCursor();
        }

        /// <summary>
        /// 实现打字机效果
        /// </summary>
        public override void Render()
        {
            speakerLabel.text = base.props.speaker;
            if (states.displayedText.Length < CurrentLineLength())
            {
                states.displayedText = CurrentLine()[..(states.displayedText.Length + 1)];
                contentLabel.text = states.displayedText;
            }

            
            if (states.displayedText.Length == CurrentLineLength())
            {
                OnDisplayFullCotent.Invoke();
                if (states.currentLine >= props.lines.Length - 1)
                {
                    timer.Clear();
                }
            }
        }

        private string CurrentLine()
        {
            return base.props.lines[states.currentLine];
        }

        private int CurrentLineLength()
        {
            return base.props.lines[states.currentLine].Length;
        }

        #region [[Inherited Fields]]
        public override string Address => "PUI_DialoguePannel";
        protected override bool NeedPreload => true;
        public override bool IsResident => true;
        public override bool BlockPlayerControl => true;
        public override Props DefaultProps { get; }
        #endregion
    }
}