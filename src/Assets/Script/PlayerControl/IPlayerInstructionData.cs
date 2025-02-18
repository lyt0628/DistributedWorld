

namespace QS.Api.Data
{
    /// <summary>
    /// IPlayerInstructionService takes responsibility for provideing instructions from player.
    /// Service should not directly accept infomation from user to trigger response for instruction.
    /// <seealso cref="Data.IPlayerInputData"/>
    /// </summary>
    public interface IPlayerInstructionData
    {
        bool Jump { get; }
    }
}