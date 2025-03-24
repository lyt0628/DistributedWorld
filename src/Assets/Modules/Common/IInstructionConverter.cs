

using QS.Api.Executor.Domain;
using System;

namespace QS.Common
{
    /// <summary>
    /// 不支持一转换多
    /// </summary>
    public interface IInstructionConverter
    {
        IInstruction Convert(IInstruction instruction);
        string AddConversion(Func<IInstruction, bool> condition, Func<IInstruction, IInstruction> conversion);
        void RemoveConversion(string uuid);
    }
}