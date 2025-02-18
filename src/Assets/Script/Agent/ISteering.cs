


using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;

namespace QS.Agent
{
    /// <summary>
    /// 指示移動的角色，封裝了移動AI的算法
    /// </summary>
    interface ISteering
    {
        ICharaControlInstr GetTranslateInstr();
    }
}