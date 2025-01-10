


using GameLib.DI;
using QS.Api.Chara.Service;
using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.Chara.Domain.Handler;
using QS.Executor.Domain.Handler;
using QS.GameLib.Pattern.Pipeline;
using UnityEngine;

namespace QS.Chara.Service
{
    class CharaAblityFactory : ICharaAblityFactory
    {
        // 不要静态的在代码中引用单例, 为什么会出问题???? 内存无法释放 TLS
        //readonly IDIContext context = CharaGlobal.Instance.DI;



        public IInstructionHandler Translate(IRelayExecutor executor, Transform transform, Animator animator)
        {
            return new TranslateAbility(executor, transform, animator);
        }
    }
}