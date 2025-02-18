using GameLib.DI;
using QS.Api.Chara.Instruction;
using QS.Api.Combat.Domain;
using QS.Api.Combat.Service;
using QS.Api.Executor.Domain;
using QS.Executor.Domain;
using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace QS.Chara.Domain.Handler
{
    public class CombatDataChangedEvent : UnityEvent<ICombatData>
    {
    }

    /// <summary>
    /// 这里使用 ECS 模式， 这里维护数据,整个System 是Combator
    /// </summary>
    public class CombatorHandler : AbstractHandler
    {

        readonly IBuffedCombater combator;
        public CombatDataChangedEvent CombatDataChanged { get; } = new();

        public CombatorHandler(IRelayExecutor executor, IBuffedCombater combator)
            :base(executor)
        {
            this.combator = combator;
        }

        public override void Read(IPipelineHandlerContext context, object msg)
        {
            if (msg is not IInjuredInstr instr)
            {
                context.Write(msg);
                return;
            }
            
            combator.Injured(instr.Attack);
            CombatDataChanged.Invoke(combator.CombatData);
        }
    }
}
