using QS.GameLib.Pattern;

namespace QS.Api.Presentation.Interact
{
    interface IInteractTrigger : IListenable<IInteractable>
    {
        bool IsOneshot { get; }
        bool TryTrig();
    }
}