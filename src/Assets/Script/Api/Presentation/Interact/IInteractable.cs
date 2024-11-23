

namespace QS.Api.Presentation.Interact
{
    /// <summary>
    /// Anything that can be called when "Interact" Instruction recived.
    /// <seealso cref="Api.Service.IPlayerInstructionData"/>
    /// </summary>
    public interface IInteractable
    {
        void Interact();
    }
}