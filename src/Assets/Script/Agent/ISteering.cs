


using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;

namespace QS.Agent
{
    /// <summary>
    /// ָʾ�ƄӵĽ�ɫ�����b���Ƅ�AI���㷨
    /// </summary>
    interface ISteering
    {
        ICharaControlInstr GetTranslateInstr();
    }
}