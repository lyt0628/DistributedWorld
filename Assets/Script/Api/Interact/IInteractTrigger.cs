
using GameLib;

namespace QS.API
{
    interface IInteractTrigger : IListenable<IInteractable> {
        bool IsOneshot { get; }
        bool TryTrig();

    }
}