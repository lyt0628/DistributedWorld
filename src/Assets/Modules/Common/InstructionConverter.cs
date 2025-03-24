

using GameLib.DI;
using QS.Api.Executor.Domain;
using QS.GameLib.Util;
using System;
using System.Collections.Generic;

namespace QS.Common
{
    [Scope(Value = ScopeFlag.Prototype)]
    class InstructionConverter : IInstructionConverter
    {
        class Conversion
        {
           public Func<IInstruction, bool> condition;

            public Conversion(Func<IInstruction, bool> condition, Func<IInstruction, IInstruction> convertor)
            {
                this.condition = condition;
                this.convertor = convertor;
            }

            public Func<IInstruction, IInstruction> convertor;
        }
        Dictionary<string, Conversion> m_Conversions = new();
        public string AddConversion(Func<IInstruction, bool> condition, 
                                  Func<IInstruction, IInstruction> conversion)
        {
            var uuid = MathUtil.UUID();
            m_Conversions.Add(uuid, new Conversion(condition, conversion));
            return uuid;
        }

        public IInstruction Convert(IInstruction instruction)
        {
            var result = instruction;
            foreach (var conv in m_Conversions.Values)
            {
                if(conv.condition(result))
                {
                    result = conv.convertor(result);
                    break;
                }
            }
            return result;
        }

        public void RemoveConversion(string uuid)
        {
            m_Conversions.Remove(uuid);
        }
    }
}