

namespace QS.Api.Data
{

    /// <summary>
    /// IPlaerInputData takes the responsibility for accepting direct input form
    /// player, and provide them to other scripts.
    /// </summary>
    public interface IPlayerInputData
    {
        float Horizontal { get; }
        float Vertical { get; }
    }
}