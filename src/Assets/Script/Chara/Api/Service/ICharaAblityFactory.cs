using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.GameLib.Pattern.Pipeline;

namespace QS.Api.Chara.Service
{
    public interface ICharaAblityFactory
    {
        IInstructionHandler Injured(IRelayExecutor executor, IInjurable injurable);
    }
}