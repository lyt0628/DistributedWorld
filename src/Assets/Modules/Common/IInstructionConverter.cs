

using QS.Api.Executor.Domain;
using System;

namespace QS.Common
{
    /// <summary>
    /// ��֧��һת����
    /// </summary>
    public interface IInstructionConverter
    {
        IInstruction Convert(IInstruction instruction);
        string AddConversion(Func<IInstruction, bool> condition, Func<IInstruction, IInstruction> conversion);
        void RemoveConversion(string uuid);
    }
}