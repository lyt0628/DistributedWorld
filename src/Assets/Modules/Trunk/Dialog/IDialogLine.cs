

namespace QS.Dialog
{
    public interface IDialogLine
    {
        SpeakerType Type { get; }
        ILine Line { get; }
        LineOptionList Options { get; }
    }
}