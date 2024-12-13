

namespace QS.Api.Presentation.Interact
{
    /// <summary>
    /// Anything that can be called when "Interact" InstantiateInstruction recived.
    /// <seealso cref="Api.Service.IPlayerInstructionData"/>
    /// </summary>
    public interface IInteractable
    {
        void Interact();
    }
}