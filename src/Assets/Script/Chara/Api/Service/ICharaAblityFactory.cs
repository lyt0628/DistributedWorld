using QS.Api.Combat.Domain;
using QS.Api.Executor.Domain;
using QS.GameLib.Pattern.Pipeline;
using UnityEngine;

namespace QS.Api.Chara.Service
{
    public interface ICharaAblityFactory
    {
        IInstructionHandler Translate(IRelayExecutor executor, Transform transform, Animator animator);
       
    }
}