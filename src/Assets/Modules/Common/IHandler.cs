using QS.Api.Executor.Domain;

namespace QS.Common
{
    public interface IHandler
    {
        bool TryHande(IInstruction instruction);

    }
}