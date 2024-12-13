


using GameLib.DI;
using QS.Api.Character.Instruction;
using QS.Api.Character.Service;
using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.Chara.Domain.Handler;
using QS.GameLib.Pattern.Pipeline;

namespace QS.Chara.Service
{
    class CharaAblityFactory : ICharaAblityFactory
    {
        // 不要静态的在代码中引用单例, 为什么会出问题???? 内存无法释放 TLS
        //readonly IDIContext context = CharaGlobal.Instance.DI;

        public IInstructionHandler Injured(IRelayExecutor executor  ,IInjurable injurable)
        {
            var h = new InjuredInstructionHandler(executor, injurable);
            CharaGlobal.Instance.DI.Inject(h);
            return h;
        }
    }
}