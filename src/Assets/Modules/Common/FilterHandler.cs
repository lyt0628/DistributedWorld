

using GameLib.DI;
using QS.Api.Executor.Domain;
using QS.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace QS.Common
{
    [Scope(Value = ScopeFlag.Prototype)]
    public class FilterHandler : IHandler, IInstructionFiler
    {
        readonly HashSet<Type> m_BlackList = new();
        public void AddToBlackList(params Type[] instrType)
        {
            m_BlackList.UnionWith(instrType);
        }

        public void AddToBlackList<T>() where T : IInstruction
        {
            m_BlackList.Add(typeof(T));
        }
        public void RemoveFromBlackList<T>() where T : IInstruction
        {
            m_BlackList.Remove(typeof(T));
        }

        public void RemoveFromBlackList(params Type[] instrType)
        {
            m_BlackList.RemoveWhere(t => instrType.Contains(t));
        }

        public bool TryHande(IInstruction instruction)
        {
            return m_BlackList.Any(t=> t == instruction.GetType() 
                                        || t.IsAssignableFrom(instruction.GetType()));
        }
    }
}