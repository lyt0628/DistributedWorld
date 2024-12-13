using GameLib.DI;
using QS.Api.Character.Instruction;
using QS.Api.Combat.Domain;
using QS.Api.Combat.Service;
using QS.Api.Executor.Domain;
using QS.Executor.Domain;
using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QS.Chara.Domain.Handler
{
    /// <summary>
    /// Combat module is not a addon for Character, and is a processor to compute 
    /// damage and difinition of combator. I hope the core computation keeping simple.
    /// Waht`s more, Combat is dependented by WorldItem module, which is a data definition module,
    /// and is not in bound of ECP(Entity-Module-Proxy). Thus, the responsiby of combator lay in 
    /// Chara module. The working way is just a ECS(Entity-Module-Proxy) pattern.
    /// </summary>
    public class InjuredInstructionHandler : AbstractHandler
    {
        [Injected]
        readonly IAttackFactory attackFactory;

        readonly IInjurable injurable;

        public InjuredInstructionHandler(IRelayExecutor executor, IInjurable injurable)
            :base(executor)
        {
            this.injurable = injurable;
        }

        public override void Read(IPipelineHandlerContext context, object msg)
        {
            if (!ReflectionUtil.IsChildOf<IInjuredInstruction>(msg))
            {
                context.Write(msg);
                return;
            }
            
            var i = (IInjuredInstruction)msg;

            injurable.Injured(attackFactory.NewAttack(i.Atk, i.Matk));
        }
    }
}
