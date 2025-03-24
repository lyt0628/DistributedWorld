

namespace QS.Dialog
{
    class DialogLine : IDialogLine
    {
        public SpeakerType Type { get; }

        public DialogLine(SpeakerType type, LineOptionList options)
        {
            Type = type;
            Options = options;
        }

        public DialogLine(SpeakerType type, ILine line)
        {
            Type = type;
            Line = line;
        }

        public ILine Line { get; } = new Line("...");

        public LineOptionList Options { get; } = new();
    }
}