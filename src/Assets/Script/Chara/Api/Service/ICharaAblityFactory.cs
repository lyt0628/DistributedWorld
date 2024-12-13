


using QS.Api.Character.Instruction;
using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.GameLib.Pattern.Pipeline;

namespace QS.Api.Character.Service
{
    public interface ICharaAblityFactory
    {
        IInstructionHandler Injured(IRelayExecutor executor,IInjurable injurable);
    }
}