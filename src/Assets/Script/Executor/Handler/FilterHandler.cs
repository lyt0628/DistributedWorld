


using QS.Api.Executor.Domain;
using QS.Executor.Instr;
using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QS.Executor.Domain.Handler
{
    class FilterHandler : AbstractHandler, IFilterHandler
    {
        /// <summary>
        /// ×èÈûºÚÃû†Î
        /// </summary>
        readonly HashSet<Type> blackList = new();
        /// <summary>
        /// ×èÈûÖ^Ô~
        /// </summary>
        readonly Dictionary<string, Predicate<object>> blockConditions = new();


        public FilterHandler(IRelayExecutor executor) : base(executor)
        {
        }

        public void AddBlockCondition(string id, Predicate<object> condition)
        {
            blockConditions.Add(id, condition);
        }

        public void AddToBlackList<T>() where T : IInstruction
        {
            blackList.Add(typeof(T));
        }

        public override void Read(IPipelineHandlerContext context, object msg)
        {
            if(msg is AddBlockedInstrsInstr addBlockedInstrsInstr)
            {
                foreach (var instr in addBlockedInstrsInstr.BlockedInstrs)
                {
                    blackList.Add(instr);
                }
            }
            if (msg is RemoveBlockedInstrsInstr removeBlockedInstrsInstr)
            {
                foreach (var instr in removeBlockedInstrsInstr.BlockedInstrs)
                {
                    blackList.Remove(instr);
                }
            }

            foreach (var type in blackList)
            {
                if (MatchBlockType(msg, type)) return;
            }
            foreach (var cond in blockConditions.Values)
            {
                if (cond(msg)) return;
            }

            context.Write(msg);

            static bool MatchBlockType(object msg, Type type)
            {
                return type.IsAssignableFrom(msg.GetType());
            }
        }

        public void RemoveBlockCondition(string id)
        {
            blockConditions.Remove(id);
        }

        public void RemoveFromBlackList<T>() where T : IInstruction
        {
           blackList.Remove(typeof(T));
        }
    }
}