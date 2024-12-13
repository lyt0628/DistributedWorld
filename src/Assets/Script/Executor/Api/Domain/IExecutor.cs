using QS.GameLib.Pattern;
using QS.GameLib.Pattern.Pipeline;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QS.Api.Executor.Domain
{
    public interface IExecutor 
    {
        /// <summary>
        /// Execute instruction
        /// </summary>
        /// <param name="instruction">The instruction to be executed</param>
        void Execute(IInstruction instruction);
    }
}
