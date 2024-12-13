


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
        // ��Ҫ��̬���ڴ��������õ���, Ϊʲô�������???? �ڴ��޷��ͷ� TLS
        //readonly IDIContext context = CharaGlobal.Instance.DI;

        public IInstructionHandler Injured(IRelayExecutor executor  ,IInjurable injurable)
        {
            var h = new InjuredInstructionHandler(executor, injurable);
            CharaGlobal.Instance.DI.Inject(h);
            return h;
        }
    }
}